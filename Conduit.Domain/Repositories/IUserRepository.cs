using Conduit.Domain.Entities;
using Conduit.Domain.ViewModels;

namespace Conduit.Domain.Repositories
{
    public interface IUserRepository
    {
        public void Add(User user);
        public User? FindUser(User user);
        public User? FindByEmail(string email);
        public User? FindByUsername(string username);
        public bool UserExist(string email);
        public void UpdateUser(User updateduser);
    }
}