using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.ViewModels
{
    public class UserForUpdateDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Bio { get; set; }
        public string? Image { get; set; }
        public UserForUpdateDto(string? username = null, string? email = null, string? password = null, string? bio = null, string? image = null)
        {
            Username = username;
            Email = email;
            Password = password;
            Bio = bio;
            Image = image;
        }
    }
}
