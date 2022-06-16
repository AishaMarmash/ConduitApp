using Conduit.Domain.Entities;

namespace Conduit.Domain.Repositories
{
    public interface IProfileRepository
    {
        public User? GetProfile(string username);
        public void FollowUser(User userFollwer, User followingUser);
        public void UnFollowUser(User userFollwer, User followingUser);
        public bool GetFollowingStatus(User userFollwer, User followingUser);
    }
}