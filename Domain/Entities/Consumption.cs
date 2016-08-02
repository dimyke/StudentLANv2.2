using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Range(1, int.MaxValue, ErrorMessage = "Dit item is niet langer voorradig.")]
        public int Stock { get; set; }
        public ConsumptionType Type{ get; set; }
    }
}
