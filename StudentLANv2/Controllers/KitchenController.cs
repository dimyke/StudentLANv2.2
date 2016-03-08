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
        OrderManager _orderManager = new OrderManager();
        public ActionResult Index()
        {
            return View(_orderManager.AllKitchenOrders().ToList());
        }

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