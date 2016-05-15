using System.Collections.Generic;
using Domain.Entities;

namespace DAL.Repositories.Contracts
{
    public interface IOrderRepository
    {
        KitchenOrder Find(int? id);
        KitchenOrder FindKitchenOrderPayment(int? id);
        WalletOrder FindWalletOrder(int? id);
        CreditOrder FindCreditOrder(int id);
        IEnumerable<WalletOrder> AllWalletOrders();
        IEnumerable<KitchenOrder> AllKitchenOrder();
        IEnumerable<KitchenOrder> AllUnfinishedKitchen();
        IEnumerable<KitchenOrder> AllFinishedKitchen();
        IEnumerable<KitchenOrder> UserOrders(string id);
        IEnumerable<KitchenOrder> UserOrdersByName(string nick);
        void CreateKitchenOrder(KitchenOrder kitchenorder);
        void UpdateOrder(int id, KitchenOrder kitchenorder);
        void CreateWalletOrder(WalletOrder order);
        void UpdateWalletOrder(int id, WalletOrder order);
        IEnumerable<CreditOrder> AllCreditOrders();
        void CreateCreditOrder(CreditOrder order);
        void UpdateCreditOrder(int id, CreditOrder kitchenorder);

        CreditOrder FindCreditForOrder(int id);

        void createOrderLine(OrderLine orderine);
        IEnumerable<OrderLine> OrderLineForOrder(int? id);
        void DeleteOrderLine(int id);

        #region tickets
        void CreateTicketOrder(TicketOrder ticketOrder);
        TicketOrder FindTicketOrder(int id);
        IEnumerable<TicketOrder> AllTicketOrders();
        IEnumerable<TicketOrder> UserTickets(string id);
        void UpdateTicketOrder(int id, TicketOrder ticketOrder);

        #endregion

    }
}
