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
    public class UserRepository : IUserRepository
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
            throw new NotImplementedException();
        }

        public void UpdateUser(string id, ApplicationUser user)
        {
            _ctx.Entry(_ctx.Users.Find(id)).CurrentValues.SetValues(user);
            _ctx.SaveChanges();
        }

        public void DeleteUser(string id)
        {
            _ctx.Users.Remove(_ctx.Users.Find(id));
            _ctx.SaveChanges();
        }

        public bool UserExists(string id)
        {
            return _ctx.Users.Any(o => o.UserName == id);
        }
        public IEnumerable<ApplicationUser> GetUsersWithFirstName(string name)
        {
            return _ctx.Users.Where(u => u.FirstName.ToLower().Contains(name.ToLower()));
        }

    }
}
