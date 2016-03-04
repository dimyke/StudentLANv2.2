using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BL.Managers;
using DAL;
using Domain.Entities;
using StudentLANv2.Models;

namespace StudentLANv2.Controllers
{
    public class KitchenOrdersController : Controller
    {
        private readonly OrderManager _orderManager = new OrderManager();
        public ActionResult Index()
        {
            return View(_orderManager.AllKitchenOrders().ToList());
        }

        public ActionResult Details(int id, int? orderLineId)
        {
            KitchenOrder kitchenOrder = _orderManager.Find(id);            
            return View(kitchenOrder);
        }
        public ActionResult Create()
        {
            OrderCreateModel newModel = new OrderCreateModel();
            ConsumptionManager _consumptionManager = new ConsumptionManager();
            newModel.Consumptions = _consumptionManager.All();
            return View(newModel);
        }
        [HttpPost]
        public ActionResult Create(OrderLine orderLine)
        {
            KitchenOrder k = new KitchenOrder();
            k.Date = DateTime.Now;
           
            OrderLine o = new OrderLine();
            o.ConsumptionId = orderLine.ConsumptionId;
            o.NumberOfItems = orderLine.NumberOfItems;

            _orderManager.CreateKitchenOrder(k);
            o.OrderId = k.OrderId;
            o.ConsumptionId = 1;

            _orderManager.CreateOrderLine(o);

            
            //k.OrderLines.Add(o);
            return RedirectToAction("Details", new { id = k.OrderId }); 
        }

        // GET: KitchenOrders/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: KitchenOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "OrderId,Date,TotalAmount,Completed,Deleted,ApplicationUserId")] KitchenOrder kitchenOrder)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _orderManager.CreateKitchenOrder(kitchenOrder);
        //        return RedirectToAction("Index");
        //    }
        //    return View(kitchenOrder);
        //}

        //// GET: KitchenOrders/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    KitchenOrder kitchenOrder = db.KitchenOrders.Find(id);
        //    if (kitchenOrder == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ApplicationUserId = new SelectList(db.ApplicationUser, "Id", "UserName", kitchenOrder.ApplicationUserId);
        //    return View(kitchenOrder);
        //}

        //// POST: KitchenOrders/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "OrderId,Date,TotalAmount,Completed,Deleted,ApplicationUserId")] KitchenOrder kitchenOrder)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(kitchenOrder).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ApplicationUserId = new SelectList(db.ApplicationUser, "Id", "UserName", kitchenOrder.ApplicationUserId);
        //    return View(kitchenOrder);
        //}

        //// GET: KitchenOrders/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    KitchenOrder kitchenOrder = db.KitchenOrders.Find(id);
        //    if (kitchenOrder == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(kitchenOrder);
        //}

        //// POST: KitchenOrders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    KitchenOrder kitchenOrder = db.KitchenOrders.Find(id);
        //    db.KitchenOrders.Remove(kitchenOrder);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
