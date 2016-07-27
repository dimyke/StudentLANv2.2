﻿using DAL.Repositories.Contracts;
using DAL.Repositories.EntitiyFramework;
using Domain.Entities;

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

        public Payment PayWithWallet(int orderid,double amount, string userid)
        {
            
            var user = _userManager.Find(userid);
            //if (!(user.Wallet >= amount));

            var walletPayment = new Payment()
            {
                Amount = amount,
                ApplicationUserId = userid,
                // OrderID = orderid,
                Type = PaymentSort.Wallet
            };

            CreatePayment(walletPayment);
            _userManager.Pay(amount, userid);
            return walletPayment;   
        }

        private bool checkuser(string applicationUserId, string id)
        {
            if (applicationUserId == id){return true;}else { return false; };
        }



    }
}
