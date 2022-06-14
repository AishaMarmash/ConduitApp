using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Conduit.Domain.Services;
using Microsoft.Extensions.Primitives;

namespace Conduit.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _secret;
        private readonly string _expDate;
        private static IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            _expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
            _config = config;
        }

        public string GenerateSecurityToken(string email)
        {
            var jwt = new JwtService(_config);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string GetCurrentAsync()
        {
            HttpContextAccessor httpContextAccessor = new();
            var authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["authorization"];
            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }
        //unused
        public string GetEmailClaim()
        {
            var tokenString = GetCurrentAsync();
            var tokenJwt = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var emailClaim = tokenJwt.Claims.First(c => c.Type == "email").Value;
            return emailClaim;
        }
    }
}