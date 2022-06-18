namespace Conduit.Domain.Services
{
    public interface IJwtService
    {
        public string GenerateSecurityToken(string email);
        public string GetEmailClaim();
        public string GetCurrentAsync();
    }
}