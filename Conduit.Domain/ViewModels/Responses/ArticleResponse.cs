using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.ViewModels
{
    public class ArticleResponse
    {
        public ArticleResponseDto Article { get; set; } = new();
    }
}
