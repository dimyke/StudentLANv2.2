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

        public int NumberOfItems { get; set; }
        public double PriceAmount { get; set; }

        // foreign key

        public int OrderId { get; set; }
        public int ConsumptionId { get; set; }

        // Navigational Properties

        public KitchenOrder KitchenOrder { get; set; }
        public Consumption Consumption { get; set; }


    }
}
