using DAL.Repositories.Contracts;
using DAL.Repositories.EntitiyFramework;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class OrderManager
    {
        private readonly IOrderRepository _OrderRepository = new OrderRepository();

        public KitchenOrder Find(int? id)
        {
            return _OrderRepository.Find(id);
        }

        public IEnumerable<KitchenOrder> AllKitchenOrders()
        {
            return _OrderRepository.AllKitchenOrder();
        }

        public IEnumerable<KitchenOrder> AllFinishedKitchenOrders()
        {
            return _OrderRepository.AllFinishedKitchen();
        }

        public IEnumerable<KitchenOrder> AllUnfinishedKitchenOrders()
        {
            return _OrderRepository.AllUnfinishedKitchen();
        }

        public void CreateKitchenOrder(KitchenOrder kitchenOrder)
        {
            _OrderRepository.CreateKitchenOrder(kitchenOrder);
        }

        public void CreateOrderLine(OrderLine orderine)
        {
            _OrderRepository.createOrderLine(orderine);
        }

        public IEnumerable<OrderLine> OrderLineForOrder(int? id)
        {
            return _OrderRepository.OrderLineForOrder(id);
        }


    }
}
