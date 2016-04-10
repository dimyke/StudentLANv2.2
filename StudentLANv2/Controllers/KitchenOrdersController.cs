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
            }
            else
            {
                k = _orderManager.Find(id);
            }
            
            return View(k);
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

            if (kitchenOrder == null)
            {
                return HttpNotFound();
            }

            newModel.KitchenOrder = kitchenOrder;
            return View(newModel);

        }

        //deel 2 + toevoegen van orderline
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrder(int id, OrderLine orderline)
        {
            KitchenOrder k = new KitchenOrder();
            k = _orderManager.Find(id);
            Consumption c = _consumptionManager.Find(orderline.ConsumptionId);
            if (ModelState.IsValid && c.Available && k.InProces == false)
            {
                OrderLine o = orderline;
                o.OrderId = id;
                double price = c.Price * orderline.NumberOfItems;
                o.PriceAmount += price;

                _orderManager.CreateOrderLine(orderline);


                k.TotalAmount += price;

                _orderManager.UpdateOrder(id, k);
                return RedirectToAction("AddOrder", new { id = k.OrderId });
            }
            return RedirectToAction("AddOrder", new { id = k.OrderId });
        }

        //delete an orderline from an order.
        // TODO: only for orders not completed. Find another way to get the price.
        public ActionResult DeleteOrderLine(int orderLineId, int orderid)
        {
            KitchenOrder k = _orderManager.Find(orderid);

            if (k.InProces == false && k.Completed == false)
            {
                k.TotalAmount -= k.OrderLines.SingleOrDefault(x => (x.OrderLineId == orderLineId)).PriceAmount;
                _orderManager.DelteOrderLine(orderLineId);
                _orderManager.UpdateOrder(k.OrderId, k);
            }
            return RedirectToAction("AddOrder", new { id = k.OrderId });
        }


        //TODO: if = inproces && role = admin then toggle else u have 2 b admin!
        public ActionResult ToggleInProces(int orderid)
        {
            KitchenOrder k = _orderManager.Find(orderid);
            k.InProces = true;
            _orderManager.UpdateOrder(orderid, k);
            return RedirectToAction("AddOrder", new { id = k.OrderId });
        }


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