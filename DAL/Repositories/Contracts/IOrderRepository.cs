using System.Collections.Generic;
using Domain.Entities;

namespace DAL.Repositories.Contracts
{
    public interface IOrderRepository
    {
        KitchenOrder Find(int? id);
        WalletOrder FindWalletOrder(int? id);
        IEnumerable<WalletOrder> AllWalletOrders();
        IEnumerable<KitchenOrder> AllKitchenOrder();
        IEnumerable<KitchenOrder> AllUnfinishedKitchen();
        IEnumerable<KitchenOrder> AllFinishedKitchen();
        IEnumerable<KitchenOrder> UserOrders(string id);
        IEnumerable<KitchenOrder> UserOrdersByName(string nick);
        void CreateKitchenOrder(KitchenOrder kitchenorder);
        void UpdateOrder(int id, KitchenOrder kitchenorder);
        void CreateWalletOrder(WalletOrder order);


        void createOrderLine(OrderLine orderine);
        IEnumerable<OrderLine> OrderLineForOrder(int? id);
        void DeleteOrderLine(int id);

    }
}
