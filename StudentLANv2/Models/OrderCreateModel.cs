using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;
namespace StudentLANv2.Models
{
    public class OrderCreateModel
    {
        public OrderLine OrderLine { get; set; }
        public IEnumerable<Consumption> Consumptions { get; set; }
    }
}