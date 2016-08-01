using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderLine
    {
        public int OrderLineId { get; set; }
        [Required(ErrorMessage = "Gelieve een aantal in te geven")]
        [Range(1, int.MaxValue, ErrorMessage = "Gelieve een positief getal in te geven")]
        public int NumberOfItems { get; set; }
        public double PriceAmount { get; set; }
        public bool Finished { get; set; }

        // foreign key

        public int OrderId { get; set; }
        [Required(ErrorMessage = "Gelieve een consumptie te kiezen")]
        public int ConsumptionId { get; set; }

        // Navigational Properties

        public KitchenOrder KitchenOrder { get; set; }
        public Consumption Consumption { get; set; }        
    }   
}
