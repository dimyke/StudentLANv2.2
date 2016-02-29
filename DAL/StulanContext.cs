using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
    public class StulanContext : DbContext
    {
        public StulanContext() : base("StulanContext")
        {

        }

      
        public DbSet<IOrder> Orders { get; set; }
        public DbSet<OrderLine> Orderlines { get; set; }
        public DbSet<Consumption> Consumptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
        }
    }
}
