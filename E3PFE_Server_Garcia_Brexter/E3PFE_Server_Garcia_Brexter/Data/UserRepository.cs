using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context; // adding the dbcontext

        public UserRepository(UserContext context) // adding the constructor
        {
            _context = context; 
        }

        public AppUser Create( AppUser appUser) // adding the user
        {
            _context.AppUsers.Add(appUser);
            appUser.Id = _context.SaveChanges();

            return appUser;
        }

        public AppUser GetByEmail(string email) // getting the user using email
        {
            return _context.AppUsers.FirstOrDefault(u => u.Email == email);
        }

        public AppUser GetById(int id) // getting the user by id
        {
            return _context.AppUsers.FirstOrDefault(u => u.Id == id);
        }
    }
}
