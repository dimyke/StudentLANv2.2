﻿using System.Collections.Generic;
using Domain.Entities;

namespace DAL.Repositories.Contracts
{
    public interface IOrderRepository
    {
        KitchenOrder Find(int? id);
        IEnumerable<KitchenOrder> AllKitchenOrder();
        IEnumerable<KitchenOrder> AllUnfinishedKitchen();
        IEnumerable<KitchenOrder> AllFinishedKitchen();
        IEnumerable<KitchenOrder> UserOrder(string userId);
        void CreateKitchenOrder(KitchenOrder kitchenorder);
        void UpdateOrder(int id, KitchenOrder kitchenorder);

        void createOrderLine(OrderLine orderine);
        IEnumerable<OrderLine> OrderLineForOrder(int? id);

    }
}
