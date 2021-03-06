﻿using DAL.Repositories.Contracts;
using DAL.Repositories.EntitiyFramework;
using Domain.Entities;
using System;

namespace BL.Managers
{
    public class PaymentManager
    {
        private readonly IPaymentRepository _paymentRepository = new PaymentRepository();
        private readonly IOrderRepository _orderRepository = new OrderRepository();
        private readonly ITicketRepository _ticketRepository = new TicketRepository();
        private UserManager _userManager = new UserManager();

        //Geeft een payment terug
        public Payment FindPayment(int id)
        {
            return _paymentRepository.FindPayment(id);
        }

        //Het aanmaken van een payment
        public void CreatePayment(Payment payment)
        {
            _paymentRepository.PaymentCreate(payment);
        }

        public Payment PayWithWallet(int orderid,double amount, ApplicationUser user)
        {
            
            

            var walletPayment = new Payment()
            {
                Amount = amount,
                ApplicationUserId = user.Id,
                // OrderID = orderid,
                Type = PaymentSort.Wallet
            };

            CreatePayment(walletPayment);
            _userManager.Pay(amount, user);
            return walletPayment;   
        }

        public void CreateCreditOrder(KitchenOrder k, int id, string userId)
        {
            // only make a creditorder when there doesn't excists one
            if (k.Paid == true && _orderRepository.FindCreditForOrder(id) == null)
            {
                CreditOrder c = new CreditOrder();
                c.Date = DateTime.Now;
                c.CreditForOrderId = id;
                c.AdminId = userId;
                c.TotalAmount -= k.TotalAmount;
                c.ApplicationUserId = k.ApplicationUserId;
                _orderRepository.CreateCreditOrder(c);
            }
        }

        private bool checkuser(string applicationUserId, string id)
        {
            if (applicationUserId == id){return true;}else { return false; };
        }



    }
}
