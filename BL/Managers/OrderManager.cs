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

        
    }
}
