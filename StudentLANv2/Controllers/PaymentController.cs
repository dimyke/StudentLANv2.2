﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BL.Managers;
using Domain.Entities;
using log4net.Repository.Hierarchy;
using Microsoft.AspNet.Identity;
using PayPal.Api;
using StudentLANv2.Models;
using Payment = PayPal.Api.Payment;


namespace StudentLANv2.Controllers
{
    public class PaymentController : Controller
    {
        private readonly OrderManager _orderManager = new OrderManager();
        private readonly PaymentManager _paymentManager = new PaymentManager();
        private readonly TicketsManager _ticketManager = new TicketsManager();
        private UserManager _userManager = new UserManager();
        private PayPal.Api.Payment payment;
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        //Verwijst door naar de view voor het zelf opladen van de wallet met paypal
        public ActionResult ChargeWalletPaypal()
        {
            return View();
        }

        //walletorders ophalen op basis vn naam
        [Authorize(Roles = "Superadmin")]
        public ActionResult WalletOrder(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(_userManager.GetUsersWithFirstName(searchString).ToList());
            }
            return View();
        }
        //walletorder aanmaken deel 1
        [Authorize(Roles = "Superadmin")]
        public ActionResult CreateWalletOrder(string id)
        {
            WalletOrder w = new WalletOrder();
            w.ApplicationUserId = id;
            w.Date = DateTime.Now;
            w.Paid = false;
            w.AdminId = User.Identity.GetUserId();
            _orderManager.CreateWalletOrder(w);
            return View(w);
        }
        //walletorder aanmaken deel 2
        [Authorize(Roles = "Superadmin")]
        [HttpPost]
        public ActionResult CreateWalletOrder(int OrderId, WalletOrder wallet)
        {
            WalletOrder w = _orderManager.GetWalletOrder(OrderId);
            w.TotalAmount = wallet.TotalAmount;
            w.Paid = true;
            _orderManager.UpdateWalletOrder(OrderId, w);
            ApplicationUser u = _userManager.Find(w.ApplicationUserId);
            u.Wallet += wallet.TotalAmount;
            _userManager.Update(w.ApplicationUserId, u);
            return RedirectToAction("CompleteCashWallerOrder", new { id = wallet.OrderId });
        }
        [Authorize(Roles = "Superadmin")]
        //wallet order afronden
        public ActionResult CompleteCashWallerOrder(int id)
        {
            WalletOrder w = _orderManager.GetWalletOrder(id);
            return View(w);
        }
        //kitchenorder betalen met je wallet
        public ActionResult PaymentWithWallet(int orderid,string type)
        {
            IOrder order;
            var user = _userManager.Find(User.Identity.GetUserId());

            if (type == "ticket")
            {
                order = _ticketManager.FindTicket(orderid);
            }
            else /*if (type == "kitchen")*/
            {
                order = _orderManager.Find(orderid);
            };
            if (user.Id != order.ApplicationUserId) { return View("FailureView"); };
            
            if (order.TotalAmount >= user.Wallet) { return View("FailureView");};

            Domain.Entities.Payment p = _paymentManager.PayWithWallet(orderid, order.TotalAmount, user);
            

            order.Payments.Add(p);
            order.Paid = true;
            // order.InProces = true;

            if (type == "ticket")
            {
                TicketOrder t = (TicketOrder)order;
                _ticketManager.UpdateOrder(orderid, t);
                _ticketManager.AdjustStock(t);
                return RedirectToAction("Index", "TicketOrders");
            }
            else /*if (type == "kitchen")*/
            {
                KitchenOrder k = (KitchenOrder)order;
                // k.InProces = true;
                _orderManager.UpdateOrder(orderid, k);
                return RedirectToAction("Index", "KitchenOrders");
            };
        }
        
        //order aanmaken voor de wallet op te laden met paypal
        public ActionResult CreatePaypalOrder(double amount)
        {
            WalletOrder k = new WalletOrder();
            k.Date = DateTime.Now;
            k.ApplicationUserId = User.Identity.GetUserId();
            k.AdminId = "paypal";
            k.TotalAmount = amount;
            _orderManager.CreateWalletOrder(k);
            return RedirectToAction("WalletPaymentWithPaypal", new { orderid = k.OrderId });

        }
        //Wallet opladen met paypal
        public ActionResult WalletPaymentWithPaypal(int orderid)
        {

            APIContext apiContext = PayPalConfiguration.GetAPIContext();
            var order = _orderManager.GetWalletOrder(orderid);

            try
            {
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {

                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                                "/Payment/WalletPaymentWithPayPal?orderid=" + orderid + "&";

                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, order);
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                PaypalLogger.Log("Error" + ex.Message);
                return View("FailureView");
            }

            // order.Payments.Add(Payment);
            order.Paid = true;
            _orderManager.UpdateWalletOrder(orderid, order);
            _userManager.ChargeWallet(order.TotalAmount, order.ApplicationUserId);
            return RedirectToAction("Details", "Account");

        }
        //Kitchenorder betalen met paypal
        public ActionResult PaymentWithPaypal(int orderid)
        {

            APIContext apiContext = PayPalConfiguration.GetAPIContext();
            var order = _orderManager.Find(orderid);

            try
            {
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {

                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                                "/Payment/PaymentWithPayPal?orderid=" + orderid + "&";

                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, order);
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                PaypalLogger.Log("Error" + ex.Message);
                return View("FailureView");
            }

            var paymentToSave = new Domain.Entities.Payment()
            {
                Amount = order.TotalAmount,
                ApplicationUserId = order.ApplicationUserId,
                // OrderID = orderid,
                Type = PaymentSort.PayPal


            };


            order.InProces = true;
            order.Paid = true;
            _paymentManager.CreatePayment(paymentToSave);
            // order.Payments.Add(paymentToSave);
            _orderManager.UpdateOrder(orderid, order);            
            return RedirectToAction("Index", "KitchenOrders");

        }
        //het effectief uitvoeren van de Paypal betaling
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        //het aanmaken van een payment voor paypal van een kitchenorder: de te betalen payment volgens paypal vereisten.
        private Payment CreatePayment(APIContext apiContext, string redirectUrl, KitchenOrder order)
        {
            //eff tijdelijk test ding
            var itemList = new ItemList() { items = new List<Item>() };

            itemList.items.Add(new Item()
            {
                name = "Bestelling van " + order.User.UserName,
                currency = "EUR",
                price = order.TotalAmount.ToString(),
                quantity = "1",
                sku = order.OrderId.ToString()


            });

            var payer = new Payer() { payment_method = "paypal" };

            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = order.TotalAmount.ToString()
            };

            var amount = new Amount()
            {
                currency = "EUR",
                total = order.TotalAmount.ToString(),
                details = details
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Betaling van een order",
                invoice_number = order.OrderId.ToString(),
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return this.payment.Create(apiContext);
        }
        //het aanmaken van een payment voor paypal van een walletorder: de te betalen payment volgens paypal vereisten.
        private Payment CreatePayment(APIContext apiContext, string redirectUrl, WalletOrder order)
        {
            //eff tijdelijk test ding
            var itemList = new ItemList() { items = new List<Item>() };

            itemList.items.Add(new Item()
            {
                name = "Oplading van Wallet",
                currency = "EUR",
                price = order.TotalAmount.ToString(),
                quantity = "1",
                sku = order.OrderId.ToString()


            });

            var payer = new Payer() { payment_method = "paypal" };

            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = order.TotalAmount.ToString()
            };

            var amount = new Amount()
            {
                currency = "EUR",
                total = order.TotalAmount.ToString(),
                details = details
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Betaling van een oplading",
                invoice_number = order.OrderId.ToString(),
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return this.payment.Create(apiContext);
        }




    }
}