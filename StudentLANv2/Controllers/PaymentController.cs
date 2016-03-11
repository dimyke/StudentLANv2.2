    using System;
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
        private UserManager _userManager = new UserManager();
        private PayPal.Api.Payment payment;
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PaymentWithWallet(int orderid)
        {
            var order = _orderManager.Find(orderid);
            var user = order.User;

            if (!(user.Wallet >= order.TotalAmount)) return View("FailureView");
            var walletPayment = new Domain.Entities.Payment()
            {
                Amount = order.TotalAmount,
                ApplicationUserId = user.Id,
                OrderID = order.OrderId,
                Type = PaymentSort.Cash
            };
            _paymentManager.CreatePayment(walletPayment);
            _userManager.Pay(order.TotalAmount, user.Id);
            order.Paid = true;
            order.InProces = true;

            _orderManager.UpdateOrder(orderid, order);
            return RedirectToAction("Index", "KitchenOrders");
        }

        public ActionResult ChargeWalletCash(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            WalletOrderModel newModel = new WalletOrderModel();
            WalletOrder order = _orderManager.GetWalletOrder(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            newModel.WalletOrder = order;
            return View(newModel);
        }

        public ActionResult CreateOrder()
        {
           WalletOrder  k = new WalletOrder();
            k.Date = DateTime.Now;
            
            _orderManager.CreateWalletOrder(k);
            return RedirectToAction("ChargeWalletCash", new { id = k.OrderId });
        }

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
                                "/Payment/PaymentWithPayPal?orderid=" + orderid +"&";

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
                OrderID = orderid,
                Type = PaymentSort.PayPal


            };

            
            order.InProces = true;
            order.Paid = true;
            _orderManager.UpdateOrder(orderid, order);
            _paymentManager.CreatePayment(paymentToSave);
            return RedirectToAction("Index", "KitchenOrders");

        }

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

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

        /*private WalletOrder CreateWalletOrder()
        {
            WalletOrder w = new WalletOrder();
            w.Date = DateTime.Now;
            w.ApplicationUserId = User.Identity.GetUserId();
        } */




    }
}