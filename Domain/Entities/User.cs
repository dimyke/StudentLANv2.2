using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public override string UserId { get; set; }
        public override string UserName { get; set; }
        public override string Email { get; set; }

        public string Avatar { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PostalCode { get; set; }
        public DateTime DateOfBirth { get; set; }
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
        public ICollection<Order> Orders { get; set; }


    }
}
