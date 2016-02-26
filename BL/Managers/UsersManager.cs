using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Collections.Generic;
using DAL;
using Domain.Entities;

namespace BL.Managers
{
    public class UsersManager: UserManager<User>
    {
        // private readonly IGebruikerRepository _gebruikerRepository = new EfGebruikerRepository();

        private UsersManager(IUserStore<User> store) : base(store)
        {
            //
        }

        public static UsersManager Create(IdentityFactoryOptions<UsersManager> options, IOwinContext context)
        {
            UsersManager manager = new UsersManager(new UserStore<User>(context.Get<StulanContext>()));

            manager.UserValidator = new UserValidator<User>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            return manager;
        }

        //public IEnumerable<User> All()
        //{
        //    return _gebruikerRepository.All();
        //}

        //public Gebruiker Find(string id)
        //{
        //    return _gebruikerRepository.Find(id);
        //}

        //public void Create(Gebruiker gebruiker)
        //{
        //    _gebruikerRepository.Create(gebruiker);
        //}

        //public void Update(string id, Gebruiker gebruiker)
        //{
        //    _gebruikerRepository.Update(id, gebruiker);
        //}

        //public void Delete(string id)
        //{
        //    _gebruikerRepository.Delete(id);
        //}
    }
}
