using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Conduit.Domain.ViewModels.RequestBody
{
    public class UpdateArticleModel
    {
        [JsonPropertyName("article")]
        public UpdateArticleDto Article;
    }
}
