using Conduit.Domain.Entities;
using Conduit.Domain.ViewModels;
namespace Conduit.Domain.Services
{
    public interface IProfileService
    {
        public User? GetProfile(string username);
        public void FollowUser(User userFollwer, User followingUser);
        public void UnFollowUser(User userFollwer, User followingUser);
        public ProfileResponse PrepareProfileResponse(User user);
        public bool GetFollowingStatus(User userFollwer, User followingUser);
        public ProfileResponseDto ApplyFollowingStatus(ProfileResponseDto response);
        public ProfileResponse GetFollowingActivityResponse(User currentUser, User neededUser);
    }
}