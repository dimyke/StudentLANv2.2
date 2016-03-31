using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Validation;

namespace DAL
{
    [DbConfigurationType(typeof(StuLanConfiguration))]
    public class StulanContext : IdentityDbContext<ApplicationUser>
    {
        public StulanContext() : base("StulanContext")
        {
           Database.SetInitializer(new StulanInitializer());
        }

        public DbSet<KitchenOrder> KitchenOrders { get; set; }
        public DbSet<WalletOrder> WalletOrders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<OrderLine> Orderlines { get; set; }
        public DbSet<Consumption> Consumptions { get; set; }
        public override IDbSet<ApplicationUser> Users { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw new FormattedDbEntityValidationException(e);
            }
        }

        public static StulanContext Create()
        {
            return new StulanContext();
        }

    }
}
