﻿using Conduit.Domain.Entities;
using Conduit.Domain.Repositories;
using Conduit.Domain.ViewModels;

namespace Conduit.Data.Repositories
{
    public class UsersRepository : IUserRepository
    {
        protected readonly AppContext _context;

        public UsersRepository(AppContext context)
        {
            _context = context;
        }
        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public User? FindUser(User user)
        {
            var result  = _context.Users.FirstOrDefault(m=>(m.Email == user.Email) && (m.Password == user.Password));
            return result;
        }
        public User? FindByEmail(string email)
        {
            var result = _context.Users.FirstOrDefault(m => (m.Email == email));
            return result;
        }
        public User? FindByUsername(string username)
        {
            var result = _context.Users.FirstOrDefault(m => (m.Username == username));
            return result;
        }
        public bool UserExist(string email)
        {
            var result = _context.Users.FirstOrDefault(m => (m.Email == email));
            if (result == null)
                return false;
            else
                return true;
        }

        public void UpdateUser(User updateduser)
        {
            _context.SaveChanges();
        }
    }
}