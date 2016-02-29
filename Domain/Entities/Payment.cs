﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    class Payment
    {
        public int PaymentId { get; set; }
        public double Amount { get; set; }
        public PaymentSort Type { get; set; }
        // foreign key
        public int OrderID { get; set; }
    }
}
