using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.ViewModels
{
    public class ListArticleResponse
    {
        public List<ArticleResponseDto> Articles { get; set; }
        public int ArticlesCount { get; set; }
    }
}