using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Domain.Entities;
using BL.Managers;
namespace StudentLANv2.Controllers
{
    public class ConsumptionsController : Controller
    {
        private StulanContext db = new StulanContext();

        private readonly ConsumptionManager _consumptionManager = new ConsumptionManager();

        // GET: Consumptions
        public ActionResult Index()
        {
            return null;
            //return View(db.Consumptions.ToList());
        }

        public ActionResult Details(int id)
        {
            Consumption consumptie = _consumptionManager.Find(id);
            return View(consumptie);
        }

        // GET: Consumptions/Details/5
        public ActionResult Details_scaffolded(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consumption consumption = db.Consumptions.Find(id);
            if (consumption == null)
            {
                return HttpNotFound();
            }
            return View(consumption);
        }

        // GET: Consumptions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Consumptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConsumptionId,Price,Name")] Consumption consumption)
        {
            if (ModelState.IsValid)
            {
                db.Consumptions.Add(consumption);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(consumption);
        }

        // GET: Consumptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consumption consumption = db.Consumptions.Find(id);
            if (consumption == null)
            {
                return HttpNotFound();
            }
            return View(consumption);
        }

        // POST: Consumptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConsumptionId,Price,Name")] Consumption consumption)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consumption).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consumption);
        }

        // GET: Consumptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consumption consumption = db.Consumptions.Find(id);
            if (consumption == null)
            {
                return HttpNotFound();
            }
            return View(consumption);
        }

        // POST: Consumptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consumption consumption = db.Consumptions.Find(id);
            db.Consumptions.Remove(consumption);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
