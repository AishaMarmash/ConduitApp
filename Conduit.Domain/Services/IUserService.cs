using Conduit.Domain.Entities;
using Conduit.Domain.ViewModels;
namespace Conduit.Domain.Services
{
    public interface IUserService
    {
        public void Add(User user);
        public User? Find(User user);
        public User? Find(string email);
        public bool UserExist(string email);
        public void UpdateUser(User userFromRepo, UserForUpdateDto userToUpdateo);
    }
}
