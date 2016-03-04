using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
    public class StulanInitializer : DropCreateDatabaseIfModelChanges<StulanContext>
    {
        protected override void Seed(StulanContext context)
        {
            var hasher = new PasswordHasher();

            #region Consumption
            var consumptions = new List<Consumption>
            {
                new Consumption {ConsumptionId=1, Name="Frieten", Price=2 },
                new Consumption {ConsumptionId=2, Name="Crocskes", Price=2 },
                new Consumption {ConsumptionId=3, Name="Kevin zijn moeder", Price=0 },
                new Consumption {ConsumptionId=4, Name="Bitterballen", Price=2 },
                new Consumption {ConsumptionId=5, Name="Pizza", Price=2 }
            };

            consumptions.ForEach(s => context.Consumptions.Add(s));
            context.SaveChanges();

            #endregion

            #region user

            var users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "dimyke",
                    Email="dimitrivantillo@gmail.com",
                    LastName ="Van Tillo",
                    FirstName ="Dimitri",
                    PostalCode ="2040",
                    DateOfBirth = new DateTime(1993, 10, 19),
                    Nickname = "dimyke",
                    Origin = "dimyke",
                    Steam = "Fraans",
                    BatlleNet = "Dimyke#6969",
                    Wargaming ="dimyke",
                    PasswordHash = hasher.HashPassword("SupahStronkP@ssword")

                },
                new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "pieterjan",
                    Email="pieterjan.kurris@gmail.com",
                    LastName ="Kurris",
                    FirstName ="Pieterjan",
                    PostalCode ="2930",
                    DateOfBirth = new DateTime(1991, 11, 8),
                    Nickname = "xelset",
                    Origin = "xelset",
                    Steam = "xelset",
                    BatlleNet = "xelset#2348",
                    Wargaming ="xelset",
                    PasswordHash = hasher.HashPassword("SupahStronkP@ssword")

                }
            };

            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            #endregion

            #region KitchenOrder
            var orders = new List<KitchenOrder>
            {
                new KitchenOrder
                {
                    OrderId = 1,
                    Date = new DateTime (2015 , 5, 8),
                    TotalAmount = 54,
                    Completed = true,
                    Deleted = false,
                    User = users.ElementAt(0)
                },

                new KitchenOrder
                {
                    OrderId = 2,
                    Date = new DateTime (2015 , 5, 8),
                    TotalAmount = 50,
                    Completed = false,
                    Deleted = false,
                    User = users.ElementAt(1)
                }
            };

            orders.ForEach(s => context.KitchenOrders.Add(s));
            context.SaveChanges();


            #endregion

            #region OrderLine 
            var orderlines = new List<OrderLine>
            {
                new OrderLine
                {
                    OrderLineId = 1,
                    NumberOfItems =2,
                    PriceAmount = 4,
                    _KitchenOrder = orders.ElementAt(0),
                    _Consumption = consumptions.ElementAt(0)                    
                },

                new OrderLine
                {
                    OrderLineId = 2,
                    NumberOfItems =3,
                    PriceAmount = 6,
                    _KitchenOrder = orders.ElementAt(0),
                    _Consumption = consumptions.ElementAt(1)
                },

                new OrderLine
                {
                    OrderLineId = 3,
                    NumberOfItems =2,
                    PriceAmount = 4,
                    _KitchenOrder = orders.ElementAt(0),
                    _Consumption = consumptions.ElementAt(2)
                },

                new OrderLine
                {
                    OrderLineId = 4,
                    NumberOfItems =2,
                    PriceAmount = 4,
                    _KitchenOrder = orders.ElementAt(1),
                    _Consumption = consumptions.ElementAt(0)
                },

                new OrderLine
                {
                    OrderLineId = 5,
                    NumberOfItems =1,
                    PriceAmount = 2,
                    _KitchenOrder = orders.ElementAt(1),
                    _Consumption = consumptions.ElementAt(1)
                },

                new OrderLine
                {
                    OrderLineId = 6,
                    NumberOfItems =2,
                    PriceAmount = 4,
                    _KitchenOrder = orders.ElementAt(1),
                    _Consumption = consumptions.ElementAt(2)
                },

            };

            orderlines.ForEach(s => context.Orderlines.Add(s));
            context.SaveChanges();

            #endregion

            #region Wallet
            var wallets = new List<Wallet>
            {
                new Wallet
                {
                    WalletId = 1,
                    Amount = 100,
                    ApplicationUser = users.ElementAt(0)
                },

                 new Wallet
                {
                    WalletId = 2,
                    Amount = 200,
                    ApplicationUser = users.ElementAt(1)
                }

            };

            wallets.ForEach(s => context.Wallets.Add(s));
            context.SaveChanges();

            #endregion

            #region Payment
            var payments = new List<Payment>
            {
                new Payment
                {
                    PaymentId = 1,
                    Amount = 1,
                    Type = PaymentSort.PayPal,
                    KitchenOrder = orders.ElementAt(0),
                    Wallet = wallets.ElementAt(0),
                    Paid = true
                },

                new Payment
                {
                    PaymentId = 2,
                    Amount = 1,
                    Type = PaymentSort.PayPal,
                    KitchenOrder = orders.ElementAt(1),
                    Wallet = wallets.ElementAt(1),
                    Paid = false
                }

            };

            #endregion
        }

    }
}
