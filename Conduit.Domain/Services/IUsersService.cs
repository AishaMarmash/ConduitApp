using Conduit.Domain.Entities;
using Conduit.Domain.ViewModels;
namespace Conduit.Domain.Services
{
    public interface IUsersService
    {
        public void Add(User user);
        public User? FindUser(User user);
        public User? FindByEmail(string email);
        public User? FindByUsername(string username);
        public bool UserExist(string email);
        public void UpdateUser(User updateduser);
        public UserResponse PrepareUserResponse(User user , string token);
    }
}
