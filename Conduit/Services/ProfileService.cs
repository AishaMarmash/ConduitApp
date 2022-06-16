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
        private IMapper _mapper;

        public ProfileService(IProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
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
        public bool FollowingStatus(User userFollwer, User followingUser)
        {
            return _profileRepository.FollowingStatus(userFollwer,followingUser);
        }
    }
}