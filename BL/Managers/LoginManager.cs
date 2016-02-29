using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using BL.Managers;
using Domain.Entities;

namespace BL.Managers
{
    public class LoginManager : SignInManager<ApplicationUser, string>
    {
        private LoginManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
            //
        }

        public static LoginManager Create(IdentityFactoryOptions<LoginManager> options, IOwinContext context)
        {
            return new LoginManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }
    }
}
