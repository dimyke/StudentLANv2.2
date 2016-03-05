using System.Collections.Generic;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.StulanContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());

        }

        protected override void Seed(DAL.StulanContext context)
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
                    PasswordHash = hasher.HashPassword("SupahStronkP@ssword"),
                    Wallet = 656065465

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
                    PasswordHash = hasher.HashPassword("SupahStronkP@ssword"),
                    Wallet = 56754


                }
            };

            users.ForEach(s => context.Users.Add(s));

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


            #endregion

            #region OrderLine 
            var orderlines = new List<OrderLine>
            {
                new OrderLine
                {
                    OrderLineId = 1,
                    NumberOfItems =2,
                    PriceAmount = 4,
                    KitchenOrder = orders.ElementAt(0),
                    Consumption = consumptions.ElementAt(0)
                },

                new OrderLine
                {
                    OrderLineId = 2,
                    NumberOfItems =3,
                    PriceAmount = 6,
                    KitchenOrder = orders.ElementAt(0),
                    Consumption = consumptions.ElementAt(1)
                },

                new OrderLine
                {
                    OrderLineId = 3,
                    NumberOfItems =2,
                    PriceAmount = 4,
                    KitchenOrder = orders.ElementAt(0),
                    Consumption = consumptions.ElementAt(2)
                },

                new OrderLine
                {
                    OrderLineId = 4,
                    NumberOfItems =2,
                    PriceAmount = 4,
                    KitchenOrder = orders.ElementAt(1),
                    Consumption = consumptions.ElementAt(0)
                },

                new OrderLine
                {
                    OrderLineId = 5,
                    NumberOfItems =1,
                    PriceAmount = 2,
                    KitchenOrder = orders.ElementAt(1),
                    Consumption = consumptions.ElementAt(1)
                },

                new OrderLine
                {
                    OrderLineId = 6,
                    NumberOfItems =2,
                    PriceAmount = 4,
                    KitchenOrder = orders.ElementAt(1),
                    Consumption = consumptions.ElementAt(2)
                },

            };

            orderlines.ForEach(s => context.Orderlines.Add(s));


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
                    User = users.ElementAt(0),
                    Paid = true
                },

                new Payment
                {
                    PaymentId = 2,
                    Amount = 1,
                    Type = PaymentSort.PayPal,
                    KitchenOrder = orders.ElementAt(1),
                    User = users.ElementAt(1),
                    Paid = false
                }

            };
            payments.ForEach(p => context.Payments.Add(p));
            #endregion

            #region UserRoles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            roleManager.Create(new IdentityRole("Superadmin"));
            roleManager.Create(new IdentityRole("Administrator"));
            roleManager.Create(new IdentityRole("Keuken Admin"));
            roleManager.Create(new IdentityRole("Deelnemer"));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            userManager.AddToRole(users.ElementAt(0).Id, "Administrator");
            userManager.AddToRole(users.ElementAt(1).Id, "Superadmin");
            #endregion

            context.SaveChanges();
        }
    }
}
