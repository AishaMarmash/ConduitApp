using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Conduit.Domain.ViewModels
{
    public class ProfileResponse
    {
        [JsonPropertyName("profile")]
        public ProfileResponseDto Profile { get; set; }
    }
}
