using Domain.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace StudentLANv2.Models
{
    public class AddOrderLineModel
    {
        public KitchenOrder KitchenOrder{ get; set; }
        public OrderLine OrderLine { get; set; }
        public IEnumerable<SelectListItem> Consumptions { get; set; }
    }
}