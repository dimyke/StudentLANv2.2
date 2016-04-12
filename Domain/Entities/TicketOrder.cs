using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TicketOrder:IOrder
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public double TotalAmount { get; set; }
        public bool Completed { get; set; }
        public bool Deleted { get; set; }
        public bool Paid { get; set; }
        public DateTime? DateEdited { get; set; }

        //FK
        public string ApplicationUserId { get; set; }
        public int TicketTypeId { get; set; }
        public int SeatId { get; set; }

        // navigation
        public TicketType TicketType { get; set; }
        public Seat Seat { get; set; }

    }
}
