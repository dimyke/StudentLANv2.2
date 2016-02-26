using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Collections.Generic;
using Domain.Entities;

namespace BL.Managers
{
    public class UserManager: UserManager<User>
    {
        // private readonly IGebruikerRepository _gebruikerRepository = new EfGebruikerRepository();

        private GebruikerManager(IUserStore<Gebruiker> store) : base(store)
        {
            //
        }

        public static GebruikerManager Create(IdentityFactoryOptions<GebruikerManager> options, IOwinContext context)
        {
            GebruikerManager manager = new GebruikerManager(new UserStore<Gebruiker>(context.Get<EfDbContext>()));

            manager.UserValidator = new UserValidator<Gebruiker>(manager)
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

        public IEnumerable<Gebruiker> All()
        {
            return _gebruikerRepository.All();
        }

        public Gebruiker Find(string id)
        {
            return _gebruikerRepository.Find(id);
        }

        public void Create(Gebruiker gebruiker)
        {
            _gebruikerRepository.Create(gebruiker);
        }

        public void Update(string id, Gebruiker gebruiker)
        {
            _gebruikerRepository.Update(id, gebruiker);
        }

        public void Delete(string id)
        {
            _gebruikerRepository.Delete(id);
        }
    }
}
