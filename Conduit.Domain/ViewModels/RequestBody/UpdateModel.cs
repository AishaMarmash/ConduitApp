using System.Text.Json.Serialization;

namespace Conduit.Domain.ViewModels
{
    public class UpdateModel
    {
        [JsonPropertyName("user")]
        public UpdateUserDto User { get; set; }
    }
}
