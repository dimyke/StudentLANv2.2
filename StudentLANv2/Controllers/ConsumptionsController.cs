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
    [Authorize(Roles = "Superadmin")]
    public class ConsumptionsController : Controller
    {
        private readonly ConsumptionManager _consumptionManager = new ConsumptionManager();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(_consumptionManager.All().ToList());
        }

        //geeft de details van een consumptie weer
        public ActionResult Details(int id)
        {
            Consumption consumptie = _consumptionManager.Find(id);
            return View(consumptie);
        }

        //Het aanpassen van een consumptie: de view
        public ActionResult Edit(int id)
        {
            Consumption consumption = _consumptionManager.Find(id);
            if (consumption == null)
            {
                return HttpNotFound();
            }
            return View(consumption);
        }

        // Het effectieve aanpassen van de consumptie
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Consumption consumption)
        {
            if (ModelState.IsValid)
            {
                _consumptionManager.Update(id, consumption);
                return RedirectToAction("Index");
            }
            return View(consumption);
        }

        public ActionResult Create()
        {
            return View();
        }

        //het aanmaken van een consumptie
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Price,Name,Available")] Consumption consumption)
        {
            if (ModelState.IsValid)
            {
                _consumptionManager.Create(consumption);
                return RedirectToAction("Index");
            }
            return View(consumption);
        }

        // Het verwijderen van een consumptie
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consumption consumption = _consumptionManager.Find(id);
            if (consumption == null)
            {
                return HttpNotFound();
            }
            return View(consumption);
        }

        //Het confirmen van het verwijderen
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _consumptionManager.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
