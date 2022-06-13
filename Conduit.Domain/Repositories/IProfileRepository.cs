using Conduit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Repositories
{
    public interface IProfileRepository
    {
        public User GetProfile(string username);
    }
}
