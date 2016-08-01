using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Consumption
    {
        public int ConsumptionId { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public Boolean Available { get; set; }
        public int Stock { get; set; }
    }
}
