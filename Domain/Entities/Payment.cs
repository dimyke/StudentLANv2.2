using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public double Amount { get; set; }
        public PaymentSort Type { get; set; }
        // foreign key
        public int OrderID { get; set; }
        public string ApplicationUserId { get; set; }

        //navigational properties
        public KitchenOrder KitchenOrder { get; set; }
        public ApplicationUser User { get; set; }
    }
}
