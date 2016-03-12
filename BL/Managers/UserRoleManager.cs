using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace BL.Managers
{
    //Zorgt voor de rollen van de gebruikers.
    class UserRoleManager : RoleManager<IdentityRole>
    {
        private UserRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {

        }

        public static UserRoleManager Create(IdentityFactoryOptions<UserRoleManager> options, IOwinContext context)
        {
            return new UserRoleManager(new RoleStore<IdentityRole>(context.Get<StulanContext>()));
        }
    }


}
