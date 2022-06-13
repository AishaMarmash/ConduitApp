using Conduit.Domain.Entities;
using Conduit.Domain.ViewModels;
namespace Conduit.Domain.Services
{
    public interface IProfileService
    {
        public User GetProfile(string username);
        public ProfileResponse PrepareProfileResponse(User user);
    }
}