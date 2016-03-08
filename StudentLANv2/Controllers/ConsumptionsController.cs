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
    

    public ActionResult Details(int id)
        {
            Consumption consumptie = _consumptionManager.Find(id);
            return View(consumptie);
        }

        public ActionResult Edit(int id)
        {
           Consumption consumption = _consumptionManager.Find(id);
            if (consumption == null)
            {
                return HttpNotFound();
            }
            return View(consumption);
        }

        // POST: KitchenOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Consumption consumption)
        {
            if (ModelState.IsValid)
            {
                _consumptionManager.Update(id, consumption);
                return RedirectToAction("Index", "Kitchen");
            }
            return View(consumption);
        }
    }
}
