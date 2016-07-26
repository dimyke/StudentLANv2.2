using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BL.Managers;
using Domain.Entities;
using StudentLANv2.Models;
using Microsoft.AspNet.Identity;

namespace StudentLANv2.Controllers
{
    [Authorize]
    public class TicketOrdersController : Controller
    {
        private readonly TicketsManager _TicketManager = new TicketsManager();

        // GET: TicketOrders
        public ActionResult Index()
        {
            return View(_TicketManager.GetUserTickets(User.Identity.GetUserId()).ToList());
        }

        public ActionResult OrderTicket()
        {
            return null;
        }

        public ActionResult CreateTicket()
        {
            TicketOrder t = new TicketOrder();
            t.Date = DateTime.Now;
            t.ApplicationUserId = User.Identity.GetUserId();

            _TicketManager.CreateTicketOrder(t);
            return RedirectToAction("AddTicket", new { id = t.OrderId });
        }
        //Deel 1 van het aanmaken van een Ticketorderline
        public ActionResult AddTicket(int? id)
        {
            TicketsMetaManager _TicketMetaManager = new TicketsMetaManager();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AddTicketLineModel newModel = new AddTicketLineModel();
            newModel.TicketTypes = GetSelectListItems(_TicketMetaManager.AllTicketTypes());
            TicketOrder ticketOrder = _TicketManager.Find(id);

            if (IsCorrectUser(ticketOrder.ApplicationUserId))
            {
                if (ticketOrder == null)
                {
                    return HttpNotFound();
                }

                newModel.TicketOrder = ticketOrder;
                return View(newModel);
            }
            else { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

        }

        //deel 2 + toevoegen van orderline
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTicket(int id, TicketLine ticketline)
        {
            _TicketManager.CreateTickeLine(id, ticketline, User.Identity.GetUserId());
            return RedirectToAction("AddTicket", id);
        }

        //delete an orderline from an order.
        public ActionResult DeleteTicketLine(int orderLineId, int orderid)
        {
            _TicketManager.DeleteTicketLine(orderLineId, orderid, User.Identity.GetUserId());
            return RedirectToAction("AddTicket", new { id = orderid });
        }


        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<TicketType> elements)
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();

            // For each string in the 'elements' variable, create a new SelectListItem object
            // that has both its Value and Text properties set to a particular value.
            // This will result in MVC rendering each item as:
            //     <option value="State Name">State Name</option>
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.TicketTypeId.ToString(),
                    Text = "€ " + element.Price.ToString() + " - " + element.Day
                });
            }

            return selectList;
        }

        private bool IsCorrectUser(string user)
        {
            if (User.Identity.GetUserId() == user)
            {
                return true;
            }
            return false;
        }

        //// GET: TicketOrders/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TicketOrder ticketOrder = db.TicketOrders.Find(id);
        //    if (ticketOrder == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(ticketOrder);
        //}

        //// POST: TicketOrders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    TicketOrder ticketOrder = db.TicketOrders.Find(id);
        //    db.TicketOrders.Remove(ticketOrder);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
