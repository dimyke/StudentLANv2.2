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

        //Haalt alle gebruikers op
        public IEnumerable<ApplicationUser> All()
        {
            return _userRepository.GetAllApplicationUsers();
        }

        //Haalt een bepaalde gebruiker op
        public ApplicationUser Find(string id)
        {
            return _userRepository.FindUser(id);
        }

        //update een bepaalde gebruiker 
        public void Update(string id, ApplicationUser user)
        {
            _userRepository.UpdateUser(id, user);
        }

        //delete een bepaalde gebruiker
        public void Delete(string id)
        {
            _userRepository.DeleteUser(id);
        }

        //laat een gebruiker zijn wallet op
        public void ChargeWallet(double amount, string id)
        {
            var user = Find(id);
            user.Wallet += amount;
            Update(id, user);
        }

        //Doe een betaling met de wallet
        public void Pay(double amount, string id)
        {
            var user = Find(id);
            user.Wallet -= amount;
            Update(id, user);
        }

        //Haal de gebruikers dynamisch op aan de hand van hun naam
        public IEnumerable<ApplicationUser> GetUsersWithFirstName(string name)
        {
            return _userRepository.GetUsersWithFirstName(name);
        }
    }
}
