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
        private readonly StulanContext _ctx = new StulanContext();

        public KitchenOrder Find(int? id)
        {
            return _ctx.KitchenOrders
                .Include("OrderLines")
                .Include("User")
                .Include("Orderlines.Consumption")
                .SingleOrDefault(x => (x.OrderId == id));
        }

        public KitchenOrder FindKitchenOrderPayment(int? id)
        {
            return _ctx.KitchenOrders
                .Include("OrderLines")
                .Include("User")
                .Include("Orderlines.Consumption")
                .Include("Payments")
                .SingleOrDefault(x => (x.OrderId == id));
        }

        public IEnumerable<KitchenOrder> AllKitchenOrder()
        {
            return _ctx.KitchenOrders
                .Include("OrderLines")
                .Include("User")
                .Include("Orderlines.Consumption")
                .AsEnumerable();
        }

        public IEnumerable<KitchenOrder> AllUnfinishedKitchen()
        {
            return _ctx.KitchenOrders
                .Where(x => (x.Completed == false))
                .Where(x => (x.InProces == true))
                .Where(x => (x.Deleted == false))
                .Include("OrderLines")
                .Include("Orderlines.Consumption")
                .Include("User")
                .AsEnumerable();
        }

        public IEnumerable<KitchenOrder> AllFinishedKitchen()
        {
            return _ctx.KitchenOrders
                .Where(x => (x.Completed == true))
                .Include("Orderlines")
                .Include("Orderlines.Consumption")
                .Include("User")
                .AsEnumerable();
        }

        public IEnumerable<KitchenOrder> UserOrders(string id)
        {
            return _ctx.KitchenOrders
                .Include("User")
                .Where(x => (x.ApplicationUserId == id))
                .AsEnumerable();
        }

        public IEnumerable<KitchenOrder> UserOrdersByName(string nick)
        {
            return _ctx.KitchenOrders
                .Include("User")
                .Where(x => (x.User.UserName == nick))
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

        public void DeleteOrderLine(int id)
        {
            _ctx.Orderlines.Remove(_ctx.Orderlines.Find(id));
            _ctx.SaveChanges();
        }

        public WalletOrder FindWalletOrder(int? id)
        {
            return _ctx.WalletOrders
                .Include("User")
                .SingleOrDefault(x => (x.OrderId == id));
        }

        public IEnumerable<WalletOrder> AllWalletOrders()
        {
            return _ctx.WalletOrders
                .Include("User")
                .AsEnumerable();
        }

        public void CreateWalletOrder(WalletOrder order)
        {
            _ctx.WalletOrders.Add(order);
            _ctx.SaveChanges();
        }

        public void UpdateWalletOrder(int id, WalletOrder order)
        {
            _ctx.Entry(_ctx.WalletOrders.Find(id)).CurrentValues.SetValues(order);
            _ctx.SaveChanges();
        }

        public IEnumerable<CreditOrder> AllCreditOrders()
        {
            return _ctx.CreditOrders
                .Include("User")
                .Include("Admin")
                .AsEnumerable();
        }
        public void CreateCreditOrder(CreditOrder order)
        {
            _ctx.CreditOrders.Add(order);
            _ctx.SaveChanges();
        }


    }
}
