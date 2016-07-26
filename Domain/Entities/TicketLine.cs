using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TicketLine
    {
        public int TicketLineId { get; set; }
        [Required(ErrorMessage = "Gelieve een aantal in te geven")]
        // [Range(1, int.MaxValue, ErrorMessage = "Je moet wel een possitief getal ingeven hé makker!")]
        public int NumberOfItems { get; set; }
        public double PriceAmount { get; set; }

        // foreign key

        public int OrderId { get; set; }
        // [Required(ErrorMessage = "Gelieve een consumptie te kiezen")]
        public int TicketTypeId { get; set; }

        // Navigational Properties

        public TicketOrder TicketOrder { get; set; }
        public TicketType TicketType { get; set; }
    }
}
