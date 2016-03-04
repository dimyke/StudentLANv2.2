using DAL.Repositories.Contracts;
using DAL.Repositories.EntitiyFramework;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class PaymentManager
    {
        private readonly IPaymentRepository _paymentRepository = new PaymentRepository();

        public Payment FindPayment(int id)
        {
            return _paymentRepository.FindPayment(id);
        }

        public void CreatePayment(Payment payment)
        {
            _paymentRepository.PaymentCreate(payment);
        }

        public void Betaal(string id, double amount)
        {
            throw new NotImplementedException();
        }



    }
}
