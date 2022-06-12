using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Models
{
    public class RegisterModel
    {
        public String email { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
    }
}
