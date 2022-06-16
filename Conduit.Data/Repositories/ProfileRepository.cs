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
        public User? GetProfile(string username)
        {
            var result = _context.Users.FirstOrDefault(m => (m.Username == username));
            return result;
        }
        public void FollowUser(User userFollwer, User followingUser)
        {
            _context.Users.First(u => u.Id == userFollwer.Id).Followings.Add(followingUser);
            _context.SaveChanges();
        }
        public void UnFollowUser(User userFollwer, User followingUser)
        {
            _context.Users.Include(u=>u.Followings).First(u => u.Id == userFollwer.Id).Followings.Remove(followingUser);
            _context.SaveChanges();
        }
        public bool GetFollowingStatus(User userFollwer, User followingUser)
        {
            bool followingStatus = _context.Users.Include(u => u.Followings).First(u => u.Id == userFollwer.Id).Followings.Any(f => f.Id == followingUser.Id);
            return followingStatus;
        }
    }
}