using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.Repositories;
using Conduit.Domain.Services;
using Conduit.Domain.ViewModels;

namespace Conduit.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private IMapper _mapper;

        public UsersService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public void Add(User user)
        {
            _userRepository.Add(user);
        }

        public User? FindUser(User user)
        {
            return _userRepository.FindUser(user);
        }

        public User? FindByEmail(string email)
        {
            return _userRepository.FindByEmail(email);
        }
        public User? FindByUsername(string username)
        {
            return _userRepository.FindByUsername(username);
        }
        public void UpdateUser(User updateduser)
        {
            _userRepository.UpdateUser(updateduser);
        }

        public bool UserExist(string email)
        {
            return _userRepository.UserExist(email);
        }

        public UserResponse PrepareUserResponse(User user, string token)
        {
            UserResponseDto userResponse = _mapper.Map<UserResponseDto>(user);
            UserResponse response = new();
            response.User = userResponse;
            response.User.Token = token;
            return response;
        }
    }
}
