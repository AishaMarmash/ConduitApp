using Conduit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Data.Repositories
{
    public class ProfileRepository
    {
        protected readonly UserContext _context;

        public ProfileRepository(UserContext context)
        {
            _context = context;
        }

        public User GetUser(string username)
        {
            var result = _context.Users.SingleOrDefault(m => (m.Username == username));
            return result;
        }

    }
}
