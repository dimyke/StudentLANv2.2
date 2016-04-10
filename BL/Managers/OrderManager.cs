﻿using DAL.Repositories.Contracts;
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

        //haalt een kitchenorder op adh van id
        public KitchenOrder Find(int? id)
        {
            return _OrderRepository.Find(id);
        }

        public KitchenOrder FindKitchenOrderPayment(int? id)
        {
            return _OrderRepository.FindKitchenOrderPayment(id);
        }
        //haalt alle kitchenorders op
        public IEnumerable<KitchenOrder> AllKitchenOrders()
        {
            return _OrderRepository.AllKitchenOrder();
        }

        //haalt alle afgewerkte kitchenorders op
        public IEnumerable<KitchenOrder> AllFinishedKitchenOrders()
        {
            return _OrderRepository.AllFinishedKitchen();
        }

        //haalt alle onafgewerkte kitchenorders op
        public IEnumerable<KitchenOrder> AllUnfinishedKitchenOrders()
        {
            return _OrderRepository.AllUnfinishedKitchen();
        }

        //Maakt een kitchenorder aan
        public void CreateKitchenOrder(KitchenOrder kitchenOrder)
        {
            _OrderRepository.CreateKitchenOrder(kitchenOrder);
        }

        //maakt een orderline aan (een bepaalde consumptie voor kitchenorder x hoeveelheid)
        public void CreateOrderLine(OrderLine orderline)
        {
            _OrderRepository.createOrderLine(orderline);
        }

        //geeft de orderlines voor een bepaalde order terug
        public IEnumerable<OrderLine> OrderLineForOrder(int? id)
        {
            return _OrderRepository.OrderLineForOrder(id);
        }

        //Een order op finished zetten
        public void SetFinished(int id)
        {
            var order = Find(id);
            order.Completed = true;
            UpdateOrder(id, order);

        }

        //Een order op deleted zetten of undeleted
        public void ToggleDeleted(int id)
        {
            var order = Find(id);

            order.Deleted = order.Deleted != true;
        }

        //een kitchenorder updaten
        public void UpdateOrder(int id, KitchenOrder order)
        {
            _OrderRepository.UpdateOrder(id, order);
        }

        //Alle kitchenorders van een user ophalen
        public IEnumerable<KitchenOrder> GetUserOrders(string id)
        {
            return _OrderRepository.UserOrders(id);
        }

        //Alle orders van een user adh naam ophalen
        public IEnumerable<KitchenOrder> GetUserOrdersByName(string nick)
        {
            return _OrderRepository.UserOrdersByName(nick);
        }

        //orderline verwijderen
        public void DelteOrderLine(int id)
        {
            _OrderRepository.DeleteOrderLine(id);
        }

        //Een order aanmaken voor de wallet op te laden
        public void CreateWalletOrder(WalletOrder order)
        {
            _OrderRepository.CreateWalletOrder(order);
        }

        //Een walletorder ophalen
        public WalletOrder GetWalletOrder(int? orderid)
        {
            return _OrderRepository.FindWalletOrder(orderid);
        }

        //Alle Walletorders ophalen
        public IEnumerable<WalletOrder> GetWalletOrders()
        {
            return _OrderRepository.AllWalletOrders();
        }

        //Een walletorder updaten
        public void UpdateWalletOrder(int id, WalletOrder order)
        {
            _OrderRepository.UpdateWalletOrder(id, order);
        }

    }
}
