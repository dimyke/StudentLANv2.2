using BL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentLANv2.Controllers
{
    public class KitchenController : Controller
    {
        // GET: Kitchen
        OrderManager _orderManager = new OrderManager();
        public ActionResult KitchenView()
        {
            return View(_orderManager.AllUnfinishedKitchenOrders().ToList());
        }

        [HttpPost]
        public ActionResult FinishOrder(int orderId)
        {
            _orderManager.SetFinished(orderId);
            return RedirectToAction("KitchenView");
        }
    }
}