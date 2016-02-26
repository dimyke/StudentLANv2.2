using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderLine
    {
        public int OrderLineId { get; set; }

        public int Number { get; set; }

        public int ConsumptionId { get; set; }

        public int OrderId { get; set; }


    }
}
