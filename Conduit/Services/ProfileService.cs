using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.Repositories;
using Conduit.Domain.Services;
using Conduit.Domain.ViewModels;

namespace Conduit.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        IUsersService _usersService;
        private IMapper _mapper;

        public ProfileService(IProfileRepository profileRepository,IUsersService usersService, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _usersService = usersService;
            _mapper = mapper;
        }
        public User? GetProfile(string username)
        {
            return _profileRepository.GetProfile(username);
        }
        public void FollowUser(User follower, User followingUser)
        {
            _profileRepository.FollowUser(follower,followingUser);
        }
        public void UnFollowUser(User follower, User followingUser)
        {
            _profileRepository.UnFollowUser(follower, followingUser);
        }
        public ProfileResponse PrepareProfileResponse(User user)
        {
            ProfileResponseDto profile = _mapper.Map<ProfileResponseDto>(user);
            ProfileResponse response = new();
            response.Profile = profile;
            return response;
        }
        public bool GetFollowingStatus(User userFollwer, User followingUser)
        {
            return _profileRepository.GetFollowingStatus(userFollwer,followingUser);
        }
        public ProfileResponseDto ApplyFollowingStatus(ProfileResponseDto response)
        {
            var userEmail = _usersService.GetCurrentUserEmail();
            var currentUser = _usersService.GetUserByEmail(userEmail);
            var otherUser = _usersService.GetUserByName(response.Username);
            response.Following = GetFollowingStatus(currentUser, otherUser);
            return response;
        }
        public ProfileResponse GetFollowingActivityResponse(User currentUser, User neededUser)
        {
            var user = GetProfile(neededUser.Username);
            var response = PrepareProfileResponse(user);
            response.Profile.Following = GetFollowingStatus(currentUser, neededUser);
            return response;
        }
    }
}