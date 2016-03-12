using BL.Managers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StudentLANv2.Controllers
{
    [Authorize(Roles = "Keuken Admin, Superadmin")]
    public class KitchenAdminController : Controller
    {
        OrderManager _orderManager = new OrderManager();

        //Het deleten of undeleten van een order
        public ActionResult ToggleDelete(int orderid)
        {
            KitchenOrder k = _orderManager.Find(orderid); ;
            if (k.Deleted)
            {
                k.Deleted = false;
            }
            else
            {
                k.Deleted = true;
            }
            _orderManager.UpdateOrder(orderid, k);
            return RedirectToAction("Index", "Kitchen");
        }

        //Het aanpassen van een order
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KitchenOrder kitchenOrder = _orderManager.Find(id);
            if (kitchenOrder == null)
            {
                return HttpNotFound();
            }
            return View(kitchenOrder);
        }

        // Het updaten van een order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, KitchenOrder kitchenOrder)
        {
            if (ModelState.IsValid)
            {
                kitchenOrder.DateEdited = DateTime.Now;
                _orderManager.UpdateOrder(id, kitchenOrder);
                return RedirectToAction("Index", "Kitchen");
            }
            return View(kitchenOrder);
        }
    }
}