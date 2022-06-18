using Conduit.Domain.Entities;
using Conduit.Domain.ViewModels;

namespace Conduit.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task RegisterUser(User user);
        public User? LoginUser(User user);
        public User? GetUserByEmail(string email);
        public User? FindByUsername(string username);
        public bool UserExist(string? email = null, string? username = null);
        public void SaveChanges();
    }
}