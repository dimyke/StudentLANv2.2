using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public override string Id { get; set; }
        public override string UserName { get; set; }
        public override string Email { get; set; }

        public string Avatar { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PostalCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Origin { get; set; }
        public string Steam { get; set; }
        public string Nickname { get; set; }
        public string BatlleNet { get; set; }
        public string Wargaming { get; set; }

        public bool NewsletterSubscription { get; set; }

        // foreign key
        // public int WalletId { get; set; }
        public int TeamId { get; set; }

        // Navigation Properties
        public ICollection<KitchenOrder> KitchenOrders { get; set; }
        public ICollection<FoodOrder> FoodOrders { get; set; }
        public ICollection<Wallet> Wallets { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            return await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            return await manager.CreateIdentityAsync(this, authenticationType);
        }

    }
}
