using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Contracts
{
    public interface ITicketRepository
    {
        #region tickets
        void CreateTicketOrder(TicketOrder ticketOrder);
        TicketOrder FindTicketOrder(int? id);
        IEnumerable<TicketOrder> AllTicketOrders();
        IEnumerable<TicketOrder> UserTickets(string id);
        void UpdateTicketOrder(int id, TicketOrder ticketOrder);
        #endregion

        void createTicketLine(TicketLine orderLine);
        IEnumerable<TicketLine> OrderLineForOrder(int? id);
        void DeleteOrderLine(int id);

    }
}
