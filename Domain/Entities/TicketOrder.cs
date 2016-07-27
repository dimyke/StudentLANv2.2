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
        // public int TicketLineId { get; set; }
        public int SeatId { get; set; }

        // navigation
        // meerdere users toevoegen aan een ticket. Bv teamlead koopt 1 ticket voor alle users en moet deze daarna toekennen aan de accounts van het team. De koper zit hier sowieso in.
        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<TicketLine> TicketLines { get; set; }
        public ICollection<Seat> Seats { get; set; }
        public ICollection<Payment> Payments { get; set; }

    }
}
