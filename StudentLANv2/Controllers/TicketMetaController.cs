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
    public class TicketMetaController : Controller
    {
        private readonly TicketsMetaManager _ticketMetaManager = new TicketsMetaManager();
        // GET: TicketTypes
        public ActionResult Index()
        {
            return View(_ticketMetaManager.AllTicketTypes().ToList());
        }


        // GET: TicketTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TicketType ticketType)
        {
            if (ModelState.IsValid)
            {
                _ticketMetaManager.CreateTicketType(ticketType);
                return RedirectToAction("Index");
            }

            return View(ticketType);
        }

        // GET: TicketTypes/Edit/5
        public ActionResult Edit(int id)
        {
            TicketType ticketType = _ticketMetaManager.FindTicketType(id);
            return View(ticketType);
        }

        // POST: TicketTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [Bind(Include = "TicketTypeId,Day,Price,Sort")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TicketType ticketType)
        {
            if (ModelState.IsValid)
            {
                _ticketMetaManager.UpdateTicketType(id, ticketType);
                return RedirectToAction("Index");
            }
            return View(ticketType);
        }

        // GET: TicketTypes/Delete/5
        public ActionResult Delete(int id)
        {
            TicketType ticketType = _ticketMetaManager.FindTicketType(id);
            return View(ticketType);
        }

        // POST: TicketTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _ticketMetaManager.DeleteTicketType(id);
            return RedirectToAction("Index");
        }

        
    }
}
