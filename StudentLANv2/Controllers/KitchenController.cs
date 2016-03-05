using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.Managers;

namespace StudentLANv2.Controllers
{
    public class KitchenController : Controller
    {

        private readonly OrderManager _orderManager = new OrderManager();
        // GET: Kitchen
         public ActionResult Index()
        {
            return View(_orderManager.AllUnfinishedKitchenOrders().ToList());
        }
    }
}