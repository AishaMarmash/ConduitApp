using Conduit.Domain.Entities;
using Conduit.Domain.ViewModels;
namespace Conduit.Domain.Services
{
    public interface IProfileService
    {
        public User GetProfile(string username);
        public ProfileResponse PrepareProfileResponse(User user);
        public User FollowUser(User userFollwer, User followingUser);
        public User UnFollowUser(User userFollwer, User followingUser);
    }
}