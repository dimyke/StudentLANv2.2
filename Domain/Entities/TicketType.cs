using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TicketType
    {
        public int TicketTypeId { get; set; }
        public TicketDay Day { get; set; }
        public double Price { get; set; }
        public Seatsort Sort { get; set; }
        [Required(ErrorMessage = "Hoeveel zijn er beschikbaar?")]
        public int Stock { get; set; }


        //fk
        public int EditionId { get; set; }

        // navigiational
        public Edition edition { get; set; }
    }
}
