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
    public class KitchenOrdersController : Controller
    {
        private readonly OrderManager _orderManager = new OrderManager();
        private readonly ConsumptionManager _consumptionManager = new ConsumptionManager();

        public ActionResult Index()
        {
            return View(_orderManager.GetUserOrders(User.Identity.GetUserId()).ToList());
        }

        // Details van een order
        public ActionResult Details(int id, int? orderLineId)
        {
            KitchenOrder k;
            if (User.IsInRole("Superadmin"))
            {
                k = _orderManager.FindKitchenOrderPayment(id);
                return View(k);
            }
            else
            {
                k = _orderManager.Find(id);
            }
            if(k.ApplicationUserId == User.Identity.GetUserId())
            {
                return View(k);
            }
            else
            {
                return HttpNotFound();
            }
            
        }
        //Wordt aangeroepen door het menu item maak order. Zorgt voor het aanmaken van een order zodat 
        public ActionResult CreateOrder()
        {
            KitchenOrder k = new KitchenOrder();
            k.Date = DateTime.Now;
            k.ApplicationUserId = User.Identity.GetUserId();

            _orderManager.CreateKitchenOrder(k);
            return RedirectToAction("AddOrder", new { id = k.OrderId });
        }
        //Deel 1 van het aanmaken van een orderline
        public ActionResult AddOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AddOrderLineModel newModel = new AddOrderLineModel();
            newModel.Consumptions = GetSelectListItems(_consumptionManager.AllAvaible());
            KitchenOrder kitchenOrder = _orderManager.Find(id);

            if (IsCorrectUser(kitchenOrder.User.Id)) {
                if (kitchenOrder == null)
                {
                    return HttpNotFound();
                }

                newModel.KitchenOrder = kitchenOrder;
                return View(newModel);
            }
            else { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

        }

        //deel 2 + toevoegen van orderline
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrder(int id, OrderLine orderline)
        {
            _orderManager.CreateOrderLine(id, orderline, User.Identity.GetUserId());
            return RedirectToAction("AddOrder", id);
        }

        //delete an orderline from an order.
        public ActionResult DeleteOrderLine(int orderLineId, int orderid)
        {
            _orderManager.DeleteOrderLine(orderLineId, orderid, User.Identity.GetUserId());
            return RedirectToAction("AddOrder", new { id = orderid });
        }


        //TODO: if = inproces && role = admin then toggle else u have 2 b admin!
        //public ActionResult ToggleInProces(int orderid)
        //{
        //    KitchenOrder k = _orderManager.Find(orderid);
        //    k.InProces = true;
        //    _orderManager.UpdateOrder(orderid, k);
        //    return RedirectToAction("AddOrder", new { id = k.OrderId });
        //}


        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<Consumption> elements)
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
                    Value = element.ConsumptionId.ToString(),
                    Text = "€ " + element.Price.ToString() + " - " + element.Name
                });
            }

            return selectList;
        }

        private bool IsCorrectUser(string user)
        {
            if(User.Identity.GetUserId() == user)
            {
                return true;
            }
            return false;
        }

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
;