using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
namespace Conduit.Middlewares
{
    public static class AuthenticationMiddleware
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var _secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("PDv7DrqznYL6nv7DrqzjnQYO9JxIsWdcjnQYL6nu0f")),
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret)),
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = true
               };
           });
            return services;
        }
    }
}