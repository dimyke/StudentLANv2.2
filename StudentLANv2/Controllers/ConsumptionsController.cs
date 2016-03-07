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
    [Authorize(Roles = "Superadmin, Administrator")]
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
    }
}
