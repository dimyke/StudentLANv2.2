using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using BL.Managers;
using Domain.Entities;

namespace JPP.BLL.Managers
{
    public class LoginManager : SignInManager<User, string>
    {
        private LoginManager(UsersManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
            //
        }

        public static LoginManager Create(IdentityFactoryOptions<LoginManager> options, IOwinContext context)
        {
            return new LoginManager(context.GetUserManager<UsersManager>(), context.Authentication);
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((UsersManager)UserManager);
        }
    }
}
