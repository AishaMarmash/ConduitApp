using Conduit.Domain.Entities;
using Conduit.Domain.Repositories;
namespace Conduit.Data.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        protected readonly AppContext _context;
        public ProfileRepository(AppContext context)
        {
            _context = context;
        }

        public User GetProfile(string username)
        {
            //var result = _context.Users.Where(m => (m.Username == username)).ToList();
            var result = _context.Users.FirstOrDefault(m => (m.Username == username));
            return result;
        }
    }
}