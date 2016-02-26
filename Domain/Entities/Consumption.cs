using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Consumption
    {
        public int consumptionId { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
    }
}
