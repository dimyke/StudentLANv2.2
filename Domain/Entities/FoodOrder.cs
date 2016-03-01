using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FoodOrder : IOrder
    {
        public int OrderId { get; set; }
        public DateTime date { get; set; }
        public double TotalAmount { get; set; }
        public int UserId { get; set; }
    }
}
