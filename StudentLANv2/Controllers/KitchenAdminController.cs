using BL.Managers;
using Domain.Entities;
using Microsoft.AspNet.Identity;
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
            _orderManager.ToggleDeleted(orderid, User.Identity.GetUserId());
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
                _orderManager.UpdateOrder(id, kitchenOrder);
                return RedirectToAction("Index", "Kitchen");
            }
            return View(kitchenOrder);
        }
                
        public ActionResult CreditIndex()
        {
            return View(_orderManager.AllCreditOrders().ToList());
        }

        public ActionResult RefundOrder(int id)
        {
            _orderManager.RefundCredit(id);
            return RedirectToAction("CreditIndex");
        }
    }
}