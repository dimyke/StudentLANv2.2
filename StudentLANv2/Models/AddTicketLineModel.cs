using Domain.Entities;
using System.Collections.Generic;
using System.Web.Mvc;
namespace StudentLANv2.Models
{
    public class AddTicketLineModel
    {
        public TicketOrder TicketOrder { get; set; }
        public TicketLine TicketLine { get; set; }
        public IEnumerable<SelectListItem> TicketTypes { get; set; }
    }
}