using Conduit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Data.Repositories
{
    public class UserRepository
    {
        protected readonly AppContext _context;

        public UserRepository(AppContext context)
        {
            _context = context;
        }

        public IEnumerable<User> ListMethod()
        {
            return _context.Users.ToList();
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
