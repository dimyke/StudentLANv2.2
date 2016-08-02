using System.Collections.Generic;
using Domain.Entities;

namespace DAL.Repositories.Contracts
{
    public interface IOrderRepository
    {
        KitchenOrder Find(int? id);
        KitchenOrder FindKitchenOrderPayment(int? id);
        OrderLine FindOrderLine(int? id);
        WalletOrder FindWalletOrder(int? id);
        CreditOrder FindCreditOrder(int id);
        CreditOrder FindCreditForOrder(int id);
        IEnumerable<WalletOrder> AllWalletOrders();
        IEnumerable<KitchenOrder> AllKitchenOrder();
        IEnumerable<KitchenOrder> AllUnfinishedKitchen();
        IEnumerable<KitchenOrder> AllFinishedKitchen();
        IEnumerable<KitchenOrder> UserOrders(string id);
        IEnumerable<KitchenOrder> UserOrdersByName(string nick);
        IEnumerable<CreditOrder> AllCreditOrders();
        IEnumerable<OrderLine> OrderLineForOrder(int? id);
        void CreateKitchenOrder(KitchenOrder kitchenorder);
        void CreateCreditOrder(CreditOrder order);
        void CreateWalletOrder(WalletOrder order);
        void createOrderLine(OrderLine orderine);
        void UpdateWalletOrder(int id, WalletOrder order);
        void UpdateOrder(int id, KitchenOrder kitchenorder);        
        void UpdateCreditOrder(int id, CreditOrder kitchenorder);
        void UpdateOrderLine(int id, OrderLine orderline);
        void DeleteOrderLine(int id);
    }
}
