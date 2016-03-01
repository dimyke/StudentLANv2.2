using System;
using System.Collections.Generic;
using DAL.Repositories.Contracts;
using Domain.Entities;
using DAL;
using System.Linq;

namespace DAL.Repositories.EntitiyFramework
{
    class OrderRepository : IOrderRepository
    {
        private StulanContext _ctx = new StulanContext();
        public IEnumerable<KitchenOrder> All()
        {
            return _ctx.KitchenOrders.AsEnumerable();
        }

        public void CreateKitchenOrder(KitchenOrder kitchenorder)
        {
            _ctx.KitchenOrders.Add(kitchenorder);
            _ctx.SaveChanges();
        }

        public void createOrderLine(OrderLine orderine)
        {
            _ctx.Orderlines.Add(orderine);
            _ctx.SaveChanges();
        }

        public void ToggleOrderDeleted(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(int id, KitchenOrder kitchenorder)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KitchenOrder> UserOrder(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
