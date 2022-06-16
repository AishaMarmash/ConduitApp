using System.Text.Json.Serialization;

namespace Conduit.Domain.ViewModels.RequestBody
{
    public class UpdateModel
    {
        [JsonPropertyName("user")]
        public UpdateUserDto User { get; set; }
    }
}
