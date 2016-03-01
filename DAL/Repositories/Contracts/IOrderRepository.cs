﻿using System.Collections.Generic;
using Domain.Entities;

namespace DAL.Repositories.Contracts
{
    public interface IOrderRepository
    {
        IEnumerable<KitchenOrder> AllKitchenOrder();
        IEnumerable<KitchenOrder> AllUnfinishedKitchen();
        IEnumerable<KitchenOrder> AllFinishedKitchen();
        IEnumerable<KitchenOrder> UserOrder(string userId);
        void CreateKitchenOrder(KitchenOrder kitchenorder);
        void UpdateOrder(int id, KitchenOrder kitchenorder);
        void ToggleOrderDeleted(int id);

        void createOrderLine(OrderLine orderine);


    }
}
