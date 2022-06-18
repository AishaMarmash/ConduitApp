using System.Text.Json.Serialization;

namespace Conduit.Domain.ViewModels
{
    public class RegisterModel
    {
        [JsonPropertyName("user")]
        public RegisterUserDto User { get; set; }
    }
}