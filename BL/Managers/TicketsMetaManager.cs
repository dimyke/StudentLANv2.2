
using DAL.Repositories.Contracts;
using DAL.Repositories.EntitiyFramework;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class TicketsMetaManager
    {
        private readonly ITicketMetaRepository _TicketMetaRepository = new TicketMetaRepository();

        #region TicketType
        public void CreateTicketType(TicketType ticketType)
        {
            _TicketMetaRepository.CreateTicketType(ticketType);
        }

        public IEnumerable<TicketType> AllTicketTypes()
        {
            return _TicketMetaRepository.AllTickettypes();
        }
        public void UpdateTicketType(int ticketTypeId, TicketType ticketType)
        {
            _TicketMetaRepository.UpdateTicketType(ticketTypeId, ticketType);
        }
        public TicketType FindTicketType(int ticketTypeId)
        {
            return(_TicketMetaRepository.FindTicketType(ticketTypeId));
        }

        public void DeleteTicketType(int id)
        {
            _TicketMetaRepository.DeleteTicketType(id);
        }

        #endregion

        #region Seats
        public void CreateSeat(Seat seat)
        {
            _TicketMetaRepository.CreateSeat(seat);
        }
        public Seat FindSeat(int seatId)
        {
            return(_TicketMetaRepository.FindSeat(seatId));
        }

        public void UpdateSeat(int seatId, Seat seat)
        {
            _TicketMetaRepository.UpdateSeat(seatId, seat);
        }

        #endregion

    }
}
