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
    [Authorize(Roles="Keuken Admin")]
    public class KitchenAdminController : Controller
    {
        // GET: KitchenAdmin
        OrderManager _orderManager = new OrderManager();

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

        // GET: KitchenOrders/Edit/5
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
            //ViewBag.ApplicationUserId = new SelectList(_orderManager. ApplicationUser, "Id", "UserName", kitchenOrder.ApplicationUserId);
            return View(kitchenOrder);
        }

        // POST: KitchenOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
    }
}