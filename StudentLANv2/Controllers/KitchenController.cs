using BL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentLANv2.Controllers
{
    [Authorize(Roles = "Keuken, Keuken Admin, Superadmin")]
    public class KitchenController : Controller
    {
        private OrderManager _orderManager = new OrderManager();

        //orders weergeven aan de hand van usernames met zoekfunctie
        public ActionResult Index(string searchString)
        {
            var orders = _orderManager.AllKitchenOrders().ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                orders = _orderManager.GetUserOrdersByName(searchString).ToList();
            }

            return View(orders);
        }

        public ActionResult KitchenView()
        {
            return View(_orderManager.AllUnfinishedKitchenOrders().ToList());
        }

        //Het afmaken van een order
        [HttpPost]
        public ActionResult FinishOrder(int orderId)
        {
            _orderManager.SetFinished(orderId);
            return RedirectToAction("KitchenView");
        }

        public ActionResult FinishOrderLine(int orderLineId)
        {
            _orderManager.SetOrderLineFinished(orderLineId);
            return RedirectToAction("KitchenView");
        }
    }
}