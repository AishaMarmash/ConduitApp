using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.Repositories;
using Conduit.Domain.Services;
using Conduit.Domain.ViewModels;
using System.IdentityModel.Tokens.Jwt;

namespace Conduit.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private IJwtService _jwtService;
        private IMapper _mapper;

        public UsersService(IUserRepository userRepository, IMapper mapper, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtService = jwtService;   
        }
        public void RegisterUser(User user)
        {
            _userRepository.RegisterUser(user);
        }
        public User? LoginUser(User user)
        {
            return _userRepository.LoginUser(user);
        }
        public User? GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }
        public User? GetUserByName(string username)
        {
            return _userRepository.FindByUsername(username);
        }
        public void SaveUserChanges()
        {
            _userRepository.SaveChanges();
        }
        public bool UserExist(string? email = null, string? username = null)
        {
            return _userRepository.UserExist(email, username);
        }
        public UserResponse PrepareUserResponse(User user, string token)
        {
            UserResponseDto userResponse = _mapper.Map<UserResponseDto>(user);
            UserResponse response = new();
            response.User = userResponse;
            response.User.Token = token;
            return response;
        }
        public string GetCurrentUserEmail()
        {
            var tokenString = _jwtService.GetCurrentAsync();
            var tokenJwt = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var userEmail = tokenJwt.Claims.First(c => c.Type == "email").Value;
            return userEmail;
        }
        public bool CheckAuthentication()
        {
            var tokenString = _jwtService.GetCurrentAsync();
            if (string.IsNullOrEmpty(tokenString))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
