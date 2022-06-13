using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Conduit.Domain.ViewModels
{
    public class RegisterModel
    {
        [JsonPropertyName("user")]
        public RegisterUserDto User { get; set; }
    }
}
