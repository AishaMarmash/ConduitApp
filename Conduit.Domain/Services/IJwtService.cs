using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Services
{
    public interface IJwtService
    {
        public string GenerateSecurityToken(string email);
        public string ExtractToken(string email);
    }
}
