using DAL.Repositories.Contracts;
using DAL.Repositories.EntitiyFramework;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BL.Managers
{
    public class UserManager
    {
        private readonly IUserRepository _userRepository = new UserRepository();

        public IEnumerable<ApplicationUser> All()
        {
            return _userRepository.GetAllApplicationUsers();
        }

        public ApplicationUser Find(string id)
        {
            return _userRepository.FindUser(id);
        }

        public void Update(string id, ApplicationUser user)
        {
            _userRepository.UpdateUser(id, user);
        }

        public void Delete(string id)
        {
            _userRepository.DeleteUser(id);
        }

        public void ChargeWallet(double amount, string id)
        {
            var user = Find(id);
            user.Wallet += amount;
            Update(id, user);
        }

        public void Pay(double amount, string id)
        {
            var user = Find(id);
            user.Wallet -= amount;
            Update(id, user);
        }

    }
}
