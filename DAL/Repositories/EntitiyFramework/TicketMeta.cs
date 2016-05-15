using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using DAL;
using System.Data.Entity;
using DAL.Repositories.Contracts;

namespace DAL.Repositories.EntitiyFramework
{
    public class TicketMeta : ITicketMeta
    {
        private readonly StulanContext _ctx = new StulanContext();
        public IEnumerable<Seat> AllSeats()
        {
            return _ctx.Seats.AsEnumerable();
        }

        public IEnumerable<TicketType> AllTickettypes()
        {
            return _ctx.TicketTypes.AsEnumerable();
        }

        public void CreateSeat(Seat seat)
        {
            _ctx.Seats.Add(seat);
            _ctx.SaveChanges();
        }

        public void CreateTicketType(TicketType ticketType)
        {
            _ctx.TicketTypes.Add(ticketType);
            _ctx.SaveChanges();
        }

        public void DeleteSeat(int id)
        {
            _ctx.Seats.Remove(_ctx.Seats.Find(id));
            _ctx.SaveChanges();
        }

        public void DeleteTicketType(int id)
        {
            _ctx.TicketTypes.Remove(_ctx.TicketTypes.Find(id));
            _ctx.SaveChanges();
        }

        public Seat FindSeat(int id)
        {
            return _ctx.Seats.Find(id);
        }

        public TicketType FindTicketType(int id)
        {
            return _ctx.TicketTypes.Find(id);
        }

        public void UpdateSeat(int id, Seat seat)
        {
            _ctx.Entry(_ctx.Seats.Find(id)).CurrentValues.SetValues(seat);
            _ctx.SaveChanges();
        }

        public void UpdateTicketType(int id, TicketType ticketType)
        {
            _ctx.Entry(_ctx.TicketTypes.Find(id)).CurrentValues.SetValues(ticketType);
            _ctx.SaveChanges();
        }
    }
}
