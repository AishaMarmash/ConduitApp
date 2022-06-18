using Conduit.Domain.Entities;
using Conduit.Domain.ViewModels;
namespace Conduit.Domain.Services
{
    public interface IUsersService
    {
        public Task RegisterUser(User user);
        public User? LoginUser(User user);
        public User? GetUserByEmail(string email);
        public User? GetUserByName(string username);
        public bool UserExist(string? email = null, string? username = null);
        public void SaveUserChanges();
        public UserResponse PrepareUserResponse(User user , string token);
        public string GetCurrentUserEmail();
        public bool CheckAuthentication();
    }
}