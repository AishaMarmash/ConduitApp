using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Conduit.Domain.ViewModels.RequestBody
{
    public class CreateArticleDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        [JsonPropertyName("tagList")]
        public List<string> Tags { get; set; }
    }
}