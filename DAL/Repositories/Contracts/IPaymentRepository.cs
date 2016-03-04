using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace DAL.Repositories.Contracts
{
    public interface IPaymentRepository
    {
        Payment FindPayment(int id);
        void PaymentCreate(Payment payment);
        void Betaal(string id, ApplicationUser user)


    }
}
