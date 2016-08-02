using DAL.Repositories.Contracts;
using DAL.Repositories.EntitiyFramework;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BL.Managers
{
    public class OrderManager
    {
        private readonly IOrderRepository _OrderRepository = new OrderRepository();
        private readonly ConsumptionManager _consumptionManager = new ConsumptionManager();

        #region orderline

        // orderine ophalen
        public OrderLine FindOrderLine(int? id)
        {
            return _OrderRepository.FindOrderLine(id);
        }

        //Een orderline op finished zetten
        public void SetOrderLineFinished(int id)
        {
            var order = FindOrderLine(id);
            order.Finished = true;
            _OrderRepository.UpdateOrderLine(id, order);

        }
        //geeft de orderlines voor een bepaalde order terug
        public IEnumerable<OrderLine> OrderLineForOrder(int? id)
        {
            return _OrderRepository.OrderLineForOrder(id);
        }

        public void CreateOrderLine(int id, OrderLine orderline, string currentUser)
        {
            KitchenOrder k = new KitchenOrder();
            k = Find(id);
            if (currentUser == k.User.Id && !k.Paid)
            {
                Consumption c = _consumptionManager.Find(orderline.ConsumptionId);
                if (c.Available && k.InProces == false)
                {
                    OrderLine o = orderline;
                    o.OrderId = id;
                    double price = c.Price * orderline.NumberOfItems;
                    o.PriceAmount += price;

                    _OrderRepository.createOrderLine(orderline);
                    k.TotalAmount += price;

                    UpdateOrder(id, k);

                    //update stock
                    c.Stock -= orderline.NumberOfItems;
                    _consumptionManager.Update(c.ConsumptionId, c);
                }
            }

        }

        //orderline verwijderen
        public void DeleteOrderLine(int orderLineId, int orderid, string currentUser)
        {
            KitchenOrder k = Find(orderid);
            if (currentUser == k.User.Id) {
                if (k.InProces == false && k.Completed == false)
                {
                    k.TotalAmount -= k.OrderLines.SingleOrDefault(x => (x.OrderLineId == orderLineId)).PriceAmount;
                    _OrderRepository.DeleteOrderLine(orderLineId);
                    UpdateOrder(k.OrderId, k);
                }
            }
                
        }

        #endregion

        #region kitchenorder
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

        //Een order op finished zetten
        public void SetFinished(int id)
        {
            var order = Find(id);
            bool proceed = true;
            foreach(var line in order.OrderLines)
            {
                if (!line.Finished)
                {
                    proceed = false;
                }
            }
            if (proceed)
            {
                order.Completed = true;
                UpdateOrder(id, order);
            }            
        }

        //Een order op deleted zetten of undeleted
        public void ToggleDeleted(int id, KitchenOrder k)
        {
            
            if (k.Deleted)
            {
                k.Deleted = false;
            }
            else
            {
                k.Deleted = true;
                
                
            }
            UpdateOrder(id, k);
        }

        //een kitchenorder updaten
        public void UpdateOrder(int id, KitchenOrder order)
        {
            order.DateEdited = DateTime.Now;
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


        #endregion

        #region creditorders
        // gets all credit orders
        public IEnumerable<CreditOrder> AllCreditOrders()
        {
            return _OrderRepository.AllCreditOrders();
        }

        // refunds the credit order to the users his wallet
        public void RefundCredit(int creditId)
        {

            CreditOrder creditOrder = _OrderRepository.FindCreditOrder(creditId);

            if(creditOrder.Paid == false)
            {
                UserManager _userManager = new UserManager();
                PaymentManager _paymentManager = new PaymentManager();

                int orderId = creditOrder.CreditForOrderId;
                KitchenOrder order = Find(orderId);
                ApplicationUser user = order.User;

                //set the order date edited to today
                creditOrder.DateEdited = DateTime.Now;
                UpdateOrder(orderId, order);

                // set credit order to paid
                creditOrder.Paid = true;
                _OrderRepository.UpdateCreditOrder(creditId, creditOrder);

                // create refund payment
                var payment = new Payment();
                payment.Amount = creditOrder.TotalAmount;
                payment.ApplicationUserId = creditOrder.ApplicationUserId;
                // payment.OrderID = orderId;
                payment.Type = PaymentSort.Wallet;
                _paymentManager.CreatePayment(payment);

                // recharge user wallet
                user.Wallet += Math.Abs(creditOrder.TotalAmount);
                _userManager.Update(user.Id, user);
            }
           
        }

        #endregion
        
        #region walletorder

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

        #endregion
    }
}
