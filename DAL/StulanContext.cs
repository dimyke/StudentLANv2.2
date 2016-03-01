using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
    public class StulanContext : IdentityDbContext<ApplicationUser>
    {
        public StulanContext() : base("StulanContext")
        {

        }

        public DbSet<KitchenOrder> KitchenOrder { get; set; }
        public DbSet<FoodOrder> FoodOrders { get; set; }
        public DbSet<OrderLine> Orderlines { get; set; }
        public DbSet<Consumption> Consumptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

        public static StulanContext Create()
        {
            return new StulanContext();
        }
    }
}
