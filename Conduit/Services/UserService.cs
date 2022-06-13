using Conduit.Domain.Entities;
using Conduit.Domain.Repositories;
using Conduit.Domain.Services;
using Conduit.Domain.ViewModels;

namespace Conduit.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void Add(User user)
        {
            _userRepository.Add(user);
        }

        public User? Find(User user)
        {
            return _userRepository.Find(user);
        }

        public User? Find(string email)
        {
            return _userRepository.Find(email);
        }

        public void UpdateUser(User userFromRepo, UserForUpdateDto userToUpdateo)
        {
            _userRepository.UpdateUser(userFromRepo, userToUpdateo);
        }

        public bool UserExist(string email)
        {
            return _userRepository.UserExist(email);
        }
    }
}
