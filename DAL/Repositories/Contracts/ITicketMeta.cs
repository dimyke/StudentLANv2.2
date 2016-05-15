using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Contracts
{
    public interface ITicketMeta
    {
        void CreateTicketType(TicketType ticketType);
        TicketType FindTicketType(int id);
        IEnumerable<TicketType> AllTickettypes();
        void UpdateTicketType(int id, TicketType ticketType);
        void DeleteTicketType(int id);

        void CreateSeat(Seat seat);
        Seat FindSeat(int id);
        IEnumerable<Seat> AllSeats();
        void UpdateSeat(int id, Seat seat);
        void DeleteSeat(int id);
    }
}
