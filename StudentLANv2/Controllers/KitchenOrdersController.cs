using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BL.Managers;
using Domain.Entities;
using StudentLANv2.Models;

namespace StudentLANv2.Controllers
{
    public class KitchenOrdersController : Controller
    {
        private readonly OrderManager _orderManager = new OrderManager();
        private readonly ConsumptionManager _consumptionManager = new ConsumptionManager();
        public ActionResult Index()
        {
            return View(_orderManager.AllKitchenOrders().ToList());
        }

        // only shows some stuff. Not important
        public ActionResult Details(int id, int? orderLineId)
        {
            KitchenOrder kitchenOrder = _orderManager.Find(id);
            return View(kitchenOrder);
        }
        // the menu item 'maak order' calls this method.
        // this method on its turn calls "addorderine"
        public ActionResult CreateOrder()
        {
            KitchenOrder k = new KitchenOrder();
            k.Date = DateTime.Now;
            _orderManager.CreateKitchenOrder(k);
            return RedirectToAction("AddOrderLine", new { id = k.OrderId });
        }

        public ActionResult AddOrderLine(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AddOrderLineModel newModel = new AddOrderLineModel();
            newModel.Consumptions = _consumptionManager.All();
            KitchenOrder kitchenOrder = _orderManager.Find(id);

            if (kitchenOrder == null)
            {
                return HttpNotFound();
            }

            newModel.KitchenOrder = kitchenOrder;
            return View(newModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrderLine(int id, OrderLine orderline)
        {
            KitchenOrder k = new KitchenOrder();
            if (ModelState.IsValid)
            {
                OrderLine o = orderline;
                o.OrderId = id;
                double price = _consumptionManager.Find(orderline.ConsumptionId).Price * orderline.NumberOfItems;
                o.PriceAmount += price;

                _orderManager.CreateOrderLine(orderline);

                k = _orderManager.Find(id);
                k.TotalAmount += price;

                _orderManager.UpdateOrder(id, k);
                return RedirectToAction("AddOrderLine", new { id = k.OrderId });
            }
            return RedirectToAction("AddOrderLine", new { id = k.OrderId });
        }

        //delete an orderline from an order.
        // TODO: only for orders not in proces , not completed
        public ActionResult DeleteOrderLine(int orderLineId, int kitchenId, double price)
        {
            KitchenOrder k = _orderManager.Find(kitchenId);
            k.TotalAmount -= price;
            _orderManager.DelteOrderLine(orderLineId);
            _orderManager.UpdateOrder(k.OrderId, k);
            return RedirectToAction("AddOrderLine", new { id = k.OrderId });
        }


        //TODO: if = inproces && role = admin then toggle else u have 2 b admin!
        public ActionResult ToggleInProces(int orderid)
        {
            KitchenOrder k = _orderManager.Find(orderid);
            k.InProces = true;
            _orderManager.UpdateOrder(orderid, k);
            return RedirectToAction("AddOrderLine", new { id = k.OrderId });
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