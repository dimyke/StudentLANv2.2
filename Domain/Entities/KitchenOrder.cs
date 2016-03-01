using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class KitchenOrder:IOrder
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public double TotalAmount { get; set; }
        public int UserId { get; set; }
        public bool Completed { get; set; }
        public bool Deleted { get; set; }
    }
}
