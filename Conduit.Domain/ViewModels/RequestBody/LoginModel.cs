using System.Text.Json.Serialization;

namespace Conduit.Domain.ViewModels
{
    public class LoginModel
    {
        [JsonPropertyName("user")]
        public LoginUserDto User { get; set; }
    }
}
