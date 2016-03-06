using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class KitchenOrder:IOrder
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public double TotalAmount { get; set; }
        public bool Completed { get; set; }
        public bool Deleted { get; set; }
        public bool Paid { get; set; }
        public bool InProces { get; set; }
        public DateTime? DateEdited { get; set; }

        //FK
        public string ApplicationUserId { get; set; }

        //navigation Properties

        public ApplicationUser User { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
