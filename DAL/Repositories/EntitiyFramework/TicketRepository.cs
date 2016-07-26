using System;
using System.Collections.Generic;
using DAL.Repositories.Contracts;
using Domain.Entities;
using DAL;
using System.Linq;
using System.Data.Entity;


namespace DAL.Repositories.EntitiyFramework
{
    public class TicketRepository : ITicketRepository
    {
        private readonly StulanContext _ctx = new StulanContext();

        #region TicketOrders
        public void CreateTicketOrder(TicketOrder ticketOrder)
        {
            _ctx.TicketOrders.Add(ticketOrder);
            _ctx.SaveChanges();
        }
        public TicketOrder FindTicketOrder(int? id)
        {
            return _ctx.TicketOrders
                .Include("TicketLines")
                .Include("TicketLines.TicketType")
                .SingleOrDefault(x => (x.OrderId == id)); ;
        }
        public IEnumerable<TicketOrder> AllTicketOrders()
        {
            return _ctx.TicketOrders.AsEnumerable();
        }
        public IEnumerable<TicketOrder> UserTickets(string id)
        {
            return _ctx.TicketOrders
                  .Include("Users")
                  .Where(x => (x.ApplicationUserId == id))
                  .AsEnumerable();
        }
        public void UpdateTicketOrder(int id, TicketOrder ticketOrder)
        {
            _ctx.Entry(_ctx.TicketOrders.Find(id)).CurrentValues.SetValues(ticketOrder);
            _ctx.SaveChanges();
        }
        #endregion

        #region OrderLine

        public void createTicketLine(TicketLine orderline)
        {
            _ctx.TicketLines.Add(orderline);
            _ctx.SaveChanges();
        }

        public IEnumerable<TicketLine> OrderLineForOrder(int? id)
        {
            return _ctx.TicketLines
                .Where(x => (x.OrderId == id))
                .AsEnumerable();
        }

        public void DeleteTicketLine(int id)
        {
            _ctx.TicketLines.Remove(_ctx.TicketLines.Find(id));
            _ctx.SaveChanges();
        }
        #endregion
    }
}
