﻿using Conduit.Domain.Entities;
using Conduit.Domain.Repositories;
using Conduit.Domain.ViewModels;

namespace Conduit.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly AppContext _context;

        public UserRepository(AppContext context)
        {
            _context = context;
        }
        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public User? Find(User user)
        {
            var result  = _context.Users.SingleOrDefault(m=>(m.Email == user.Email) && (m.Password == user.Password));
            return result;
        }
        public User? Find(string email)
        {
            var result = _context.Users.SingleOrDefault(m => (m.Email == email));
            return result;
        }
        public bool UserExist(string email)
        {
            var result = _context.Users.SingleOrDefault(m => (m.Email == email));
            if (result == null)
                return false;
            else
                return true;
        }

        public void UpdateUser(User userFromRepo,UserForUpdateDto userToUpdateo)
        {
            _context.Users.SingleOrDefault(m => (m.Email == userToUpdateo.Email));
            if (userToUpdateo.Username!=null)
                userFromRepo.Username = userToUpdateo.Username;
            if (userToUpdateo.Email != null)
                userFromRepo.Email = userToUpdateo.Email;
            if (userToUpdateo.Password != null)
                userFromRepo.Password = userToUpdateo.Password;
            if (userToUpdateo.Bio != null)
                userFromRepo.Bio = userToUpdateo.Bio;
            //_context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
