using System;
using System.Collections.Generic;
using DAL.Repositories.Contracts;
using Domain.Entities;
using DAL;
using System.Linq;
using System.Data.Entity;


namespace DAL.Repositories.EntitiyFramework
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly StulanContext _ctx = new StulanContext();


        public Payment FindPayment(int id)
        {
            return _ctx.Payments.Find(id);
        }

        public void PaymentCreate(Payment payment)
       {
           _ctx.Payments.Add(payment);
           _ctx.SaveChanges();
       }

      
    }
}
