using DAL.Repositories.Contracts;
using DAL.Repositories.EntitiyFramework;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Managers
{
    public class TicketsManager
    {
        private readonly ITicketRepository _ITicketRepository = new TicketRepository();
        private readonly ITicketMetaRepository _ITicketMetaRepository = new TicketMetaRepository();

        #region Ticket
        //haalt een ticketorder op adh van id
        public TicketOrder Find(int? id)
        {
            return _ITicketRepository.FindTicketOrder(id);
        }

        //haalt alle ticketorder op
        public IEnumerable<TicketOrder> AllTicketOrders()
        {
            return _ITicketRepository.AllTicketOrders();
        }

        //Maakt een ticketorder aan
        public void CreateTicketOrder(TicketOrder ticketOrder)
        {
            _ITicketRepository.CreateTicketOrder(ticketOrder);
        }
        public void CreateTickeLine(int id, TicketLine orderline, string currentUser)
        {
            TicketOrder t = new TicketOrder();
            t = Find(id);
            if (currentUser == t.ApplicationUserId)
            {
                TicketType tt = _ITicketMetaRepository.FindTicketType(orderline.TicketTypeId);

                if (tt.Stock >= orderline.NumberOfItems/* && k.InProces == false*/)
                {
                    TicketLine o = orderline;
                    o.OrderId = id;
                    double price = tt.Price * orderline.NumberOfItems;
                    o.PriceAmount += price;

                    _ITicketRepository.createTicketLine(orderline);
                    t.TotalAmount += price;

                    UpdateOrder(id, t);
                }
            }

        }
        //Een order op deleted zetten of undeleted
        //public void ToggleDeleted(int id, string userId)
        //{
        //    KitchenOrder k = Find(id);
        //    if (k.Deleted)
        //    {
        //        k.Deleted = false;
        //    }
        //    else
        //    {
        //        k.Deleted = true;

        //        // only make a creditorder when there doesn't excists one
        //        if (k.Paid == true && _OrderRepository.FindCreditForOrder(id) == null)
        //        {
        //            CreditOrder c = new CreditOrder();
        //            c.Date = DateTime.Now;
        //            c.CreditForOrderId = id;
        //            c.AdminId = userId;
        //            c.TotalAmount -= k.TotalAmount;
        //            c.ApplicationUserId = k.ApplicationUserId;
        //            _OrderRepository.CreateCreditOrder(c);
        //        }
        //    }
        //    UpdateOrder(id, k);
        //}

        //een ticketorder updaten
        public void UpdateOrder(int id, TicketOrder order)
        {
            order.DateEdited = DateTime.Now;
            _ITicketRepository.UpdateTicketOrder(id, order);
        }

        //Alle kitchenorders van een user ophalen
        public IEnumerable<TicketOrder> GetUserTickets(string id)
        {
            return _ITicketRepository.UserTickets(id);
        }

        //Alle orders van een user adh naam ophalen
        //public IEnumerable<KitchenOrder> GetUserOrdersByName(string nick)
        //{
        //    return _OrderRepository.UserOrdersByName(nick);
        //}


        #endregion
    }
}
