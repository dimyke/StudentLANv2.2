﻿using System;
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
                new Consumption {ConsumptionId=1, Name="Frieten", Price=2, Available = true, Stock = 100, Type = ConsumptionType.Food },
                new Consumption {ConsumptionId=2, Name="Crocskes", Price=2, Available = true, Stock = 100,Type = ConsumptionType.Food },
                new Consumption {ConsumptionId=3, Name="Kefjeuh is free", Price=0, Available = true, Stock = 100,Type = ConsumptionType.Food },
                new Consumption {ConsumptionId=4, Name="Bitterballen", Price=2, Available = true, Stock = 100,Type = ConsumptionType.Food },
                new Consumption {ConsumptionId=5, Name="Pizza", Price=2, Available = false, Stock = 100,Type = ConsumptionType.Food },
                new Consumption {ConsumptionId=5, Name="Water", Price=1, Available = true, Stock = 100,Type = ConsumptionType.Drinks },
                new Consumption {ConsumptionId=5, Name="Cola", Price=1, Available = true, Stock = 100,Type = ConsumptionType.Drinks }
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
                    Origin = "dimyke",
                    Steam = "Fraans",
                    BatlleNet = "Dimyke#6969",
                    Wargaming ="dimyke",
                    PasswordHash = hasher.HashPassword("SupahStronkP@ssword"),
                    Wallet = 656,
                    SecurityStamp = Guid.NewGuid().ToString()

                },
                new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "xelset",
                    Email="pieterjan.kurris@gmail.com",
                    LastName ="Kurris",
                    FirstName ="Pieterjan",
                    PostalCode ="2930",
                    DateOfBirth = new DateTime(1991, 11, 8),
                    Origin = "xelset",
                    Steam = "xelset",
                    BatlleNet = "xelset#2348",
                    Wargaming ="xelset",
                    PasswordHash = hasher.HashPassword("SupahStronkP@ssword"),
                    Wallet = 5675,
                    SecurityStamp = Guid.NewGuid().ToString()


                },

                new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "keuken",
                    Email="keuken@gmail.com",
                    LastName ="keuken",
                    FirstName ="keuken",
                    PostalCode ="2930",
                    DateOfBirth = new DateTime(1991, 11, 8),
                    Origin = "keuken",
                    Steam = "keuken",
                    BatlleNet = "keukent#2348",
                    Wargaming ="keuken",
                    PasswordHash = hasher.HashPassword("SupahStronkP@ssword"),
                    Wallet = 56754,
                    SecurityStamp = Guid.NewGuid().ToString()


                },

                new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "keukenadmin",
                    Email="keukenadmin@gmail.com",
                    LastName ="keuken",
                    FirstName ="keuken",
                    PostalCode ="2930",
                    DateOfBirth = new DateTime(1991, 11, 8),
                    Origin = "keuken",
                    Steam = "keuken",
                    BatlleNet = "keukent#2348",
                    Wargaming ="keuken",
                    PasswordHash = hasher.HashPassword("SupahStronkP@ssword"),
                    Wallet = 56754,
                    SecurityStamp = Guid.NewGuid().ToString()


                },

                new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "deelnemer",
                    Email="deelnemer@gmail.com",
                    LastName ="deelnemer",
                    FirstName ="deelnemer",
                    PostalCode ="2930",
                    DateOfBirth = new DateTime(1991, 11, 8),
                    Origin = "deelnemer",
                    Steam = "deelnemer",
                    BatlleNet = "deelnemer#2348",
                    Wargaming ="deelnemer",
                    PasswordHash = hasher.HashPassword("SupahStronkP@ssword"),
                    Wallet = 1,
                    SecurityStamp = Guid.NewGuid().ToString()
                }
                ,

                new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "superadmin",
                    Email="superadmin@gmail.com",
                    LastName ="superadmin",
                    FirstName ="superadmin",
                    PostalCode ="2930",
                    DateOfBirth = new DateTime(1991, 11, 8),
                    Origin = "superadmin",
                    Steam = "superadmin",
                    BatlleNet = "superadmin#2348",
                    Wargaming ="superadmin",
                    PasswordHash = hasher.HashPassword("SupahStronkP@ssword"),
                    Wallet = 508,
                    SecurityStamp = Guid.NewGuid().ToString()


                }
                ,

                new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    Email="admin@gmail.com",
                    LastName ="admin",
                    FirstName ="admin",
                    PostalCode ="2930",
                    DateOfBirth = new DateTime(1991, 11, 8),
                    Origin = "admin",
                    Steam = "admin",
                    BatlleNet = "admin#2348",
                    Wargaming ="admin",
                    PasswordHash = hasher.HashPassword("SupahStronkP@ssword"),
                    Wallet = 10,
                    SecurityStamp = Guid.NewGuid().ToString()


                },
                new ApplicationUser
                {
                    Id = "paypal",
                    UserName = "paypal",
                    Email="paypal@studentlan.be",
                    LastName ="paypal",
                    FirstName ="paypal",
                    PostalCode ="2930",
                    DateOfBirth = new DateTime(1991, 11, 8),
                    Origin = "paypal",
                    Steam = "paypal",
                    BatlleNet = "paypal",
                    Wargaming ="paypal",
                    PasswordHash = hasher.HashPassword("SupahStronkPAYPALP@ssword"),
                    Wallet = 10,
                    SecurityStamp = Guid.NewGuid().ToString()
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
                    // KitchenOrder = orders.ElementAt(0),
                },

                new Payment
                {
                    PaymentId = 2,
                    Amount = 1,
                    Type = PaymentSort.PayPal,
                    // KitchenOrder = orders.ElementAt(1),
                }

            };
            payments.ForEach(p => context.Payments.Add(p));
            #endregion

            #region Edition
            var editions = new List<Edition>
            {
                new Edition
                {
                    EditionId = 1,
                    Location = "Outpost",
                    NumberOfSeats = 40
                },
                new Edition
                {
                    EditionId = 2,
                    Location = "Outpost",
                    NumberOfSeats = 40
                },
                new Edition
                {
                    EditionId = 3,
                    Location = "Outpost",
                    NumberOfSeats = 75
                },
                new Edition
                {
                    EditionId = 4,
                    Location = "Athena",
                    NumberOfSeats = 135
                },
                new Edition
                {
                    EditionId = 5,
                    Location = "Cronos",
                    NumberOfSeats = 50
                },

                new Edition
                {
                    EditionId = 6,
                    Location = "Ramada Plaza",
                    NumberOfSeats = 168
                }
            };
            editions.ForEach(p => context.Editions.Add(p));
            #endregion

            #region tickettype
            var ticketTypes = new List<TicketType>{
                new TicketType
                {
                    EditionId = 1,
                    Day = TicketDay.EarlyBirdSaturday,
                    Price = 15,
                    Sort = Seatsort.regular,
                    Stock = 100,
                    TicketTypeId = 1
                },
                new TicketType
                {
                    EditionId = 1,
                    Day = TicketDay.EarlyBirdSunday,
                    Price = 15,
                    Sort = Seatsort.regular,
                    Stock = 100,
                    TicketTypeId = 2
                },
                new TicketType
                {
                    EditionId = 1,
                    Day = TicketDay.EarlyBirdWeekend,
                    Price = 25,
                    Sort = Seatsort.regular,
                    Stock = 100,
                    TicketTypeId = 3
                }
            };
            ticketTypes.ForEach(p => context.TicketTypes.Add(p));
            #endregion

            #region UserRoles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            roleManager.Create(new IdentityRole("Superadmin"));
            roleManager.Create(new IdentityRole("Administrator"));
            roleManager.Create(new IdentityRole("Keuken Admin"));
            roleManager.Create(new IdentityRole("Keuken"));
            roleManager.Create(new IdentityRole("Deelnemer"));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            userManager.AddToRole(users.ElementAt(0).Id, "Administrator");
            userManager.AddToRole(users.ElementAt(1).Id, "Superadmin");
            userManager.AddToRole(users.ElementAt(2).Id, "Keuken");
            userManager.AddToRole(users.ElementAt(3).Id, "Keuken Admin");
            userManager.AddToRole(users.ElementAt(4).Id, "Deelnemer");
            userManager.AddToRole(users.ElementAt(5).Id, "Superadmin");
            userManager.AddToRole(users.ElementAt(6).Id, "Administrator");
            #endregion

            context.SaveChanges();
        }
    }
}
