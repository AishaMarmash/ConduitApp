using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Conduit.Domain.ViewModels
{
    public class LoginUserDto
    {
        //[Required(ErrorMessage = "Username is required")]
        public string? Email { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
