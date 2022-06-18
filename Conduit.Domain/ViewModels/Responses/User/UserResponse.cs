using System.Text.Json.Serialization;

namespace Conduit.Domain.ViewModels
{
    public class UserResponse
    {
        [JsonPropertyName("user")]
        public UserResponseDto User { get; set; }
    }
}