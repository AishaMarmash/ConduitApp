using Conduit.Domain.Entities;
using Conduit.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

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
            var result = _context.Users.FirstOrDefault(m => (m.Username == username));
            return result;
        }
        public User FollowUser(User userFollwer, User followingUser)
        {
            _context.Users.FirstOrDefault(u => u.Id == userFollwer.Id).Followings.Add(followingUser);
            _context.SaveChanges();
            User result= new User();
            return result;
        }
        public User UnFollowUser(User userFollwer, User followingUser)
        {
            var result2 = _context.Users.Include(u=>u.Followings).FirstOrDefault(u => u.Id == userFollwer.Id).Followings.Remove(followingUser);
            _context.SaveChanges();
            User result = new User();
            return result;
        }
    }
}