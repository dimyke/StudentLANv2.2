using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public double Amount { get; set; }
        public string ApplicationUserid { get; set; }
        
        //navigation properties

        public ICollection<Payment> Payments { get; set; }

    }
}
