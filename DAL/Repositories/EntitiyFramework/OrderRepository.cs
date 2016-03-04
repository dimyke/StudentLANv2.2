using System;
using System.Collections.Generic;
using DAL.Repositories.Contracts;
using Domain.Entities;
using DAL;
using System.Linq;
using System.Data.Entity;

namespace DAL.Repositories.EntitiyFramework
{
    public class OrderRepository : IOrderRepository
    {
        private StulanContext _ctx = new StulanContext();

        public KitchenOrder Find(int? id)
        {
            // dit werkt, blijf eraf of regelet zelf!
            return _ctx.KitchenOrders
                .Include(o => o.OrderLines)
                .SingleOrDefault(x => (x.OrderId == id));
        }

        public IEnumerable<KitchenOrder> AllKitchenOrder()
        {
            return _ctx.KitchenOrders
                .Include(x => x.OrderLines)
                .AsEnumerable();
        }

        public IEnumerable<KitchenOrder> AllUnfinishedKitchen()
        {
            return _ctx.KitchenOrders
                .Where(x => (x.Completed == false))
                .Include("Orderline")
                .Include("ApplicationUser")
                .AsEnumerable();
        }

        public IEnumerable<KitchenOrder> AllFinishedKitchen()
        {
            return _ctx.KitchenOrders
                .Where(x => (x.Completed == true))
                .Include("Orderline")
                .Include("ApplicationUser")
                .AsEnumerable();
        }

        public IEnumerable<ApplicationUser> UserOrders(String id)
        {
            return _ctx.Users
                .Include("KitchenOrder")
                .Include("Orderline")
                .Where(x => (x.Id == id))
                .AsEnumerable();
                
        }
        public void CreateKitchenOrder(KitchenOrder kitchenorder)
        {
            _ctx.KitchenOrders.Add(kitchenorder);
            _ctx.SaveChanges();
        }

        public void CreateOrderLine(OrderLine orderline)
        {
            _ctx.Orderlines.Add(orderline);
            _ctx.SaveChanges();
        }

        public IEnumerable<OrderLine> OrderLineForOrder(int? id)
        {
            return _ctx.Orderlines
                .Where(x => (x.OrderId == id))
                .AsEnumerable();
        }

        public void UpdateOrder(int id, KitchenOrder kitchenorder)
        {
            _ctx.Entry(_ctx.KitchenOrders.Find(id)).CurrentValues.SetValues(kitchenorder);
            _ctx.SaveChanges();
        }

        public void createOrderLine(OrderLine orderline)
        {
            _ctx.Orderlines.Add(orderline);
            _ctx.SaveChanges();
        }

        public IEnumerable<KitchenOrder> UserOrder(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
