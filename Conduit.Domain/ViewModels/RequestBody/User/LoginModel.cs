using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Conduit.Domain.ViewModels.RequestBody
{
    public class LoginModel
    {
        [JsonPropertyName("user")]
        [Required]
        public LoginUserDto User { get; set; }
    }
}
