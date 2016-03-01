using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Domain.Entities
{
    public class FoodOrder : IOrder
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime date { get; set; }
        public double TotalAmount { get; set; }
        public int UserId { get; set; }
    }
}
