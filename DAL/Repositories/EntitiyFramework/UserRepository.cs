using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.Contracts;
using Domain.Entities;

namespace DAL.Repositories.EntitiyFramework
{
    class UserRepository : IUserRepository
    {
        private readonly StulanContext _ctx = new StulanContext();
        public ApplicationUser FindUser(string id)
        {
            return _ctx.Users.Find(id);
        }

        public IEnumerable<ApplicationUser> GetAllApplicationUsers()
        {
            return _ctx.Users
                .Include("Roles")  //kan zijn dat het identityroles moet zijn
                .Include("Logins")
                .AsEnumerable();
        }

        public IEnumerable<ApplicationUser> GetAllApplicationUsersRole(int roleId)
        {
            return _ctx.Users.Where( x => (x.))
               .Include("Roles")  
               .Include("Logins")
               .AsEnumerable();
        }

        public void UpdateUser(string id, ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string id)
        {
            throw new NotImplementedException();
        }

        public void ChargeWallet(string id, double amount)
        {
            throw new NotImplementedException();
        }
    }
}
