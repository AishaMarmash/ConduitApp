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
        protected readonly AppContext _context;

        public ProfileRepository(AppContext context)
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
