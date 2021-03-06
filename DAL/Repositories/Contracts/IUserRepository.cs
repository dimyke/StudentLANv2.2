﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;


namespace DAL.Repositories.Contracts
{
    public interface IUserRepository
    {
        ApplicationUser FindUser(string id);
        bool UserExists(string id);
        IEnumerable<ApplicationUser> GetAllApplicationUsers();
        IEnumerable<ApplicationUser> GetAllApplicationUsersRole(int roleId);
        void UpdateUser(string id, ApplicationUser user);
        void DeleteUser(string id);
        IEnumerable<ApplicationUser> GetUsersWithFirstName(string name);
    }
}
