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




    }
}
