using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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