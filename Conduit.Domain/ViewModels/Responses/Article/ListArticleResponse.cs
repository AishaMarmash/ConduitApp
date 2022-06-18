namespace Conduit.Domain.ViewModels
{
    public class ListArticleResponse
    {
        public List<ArticleResponseDto> Articles { get; set; }
        public int ArticlesCount { get; set; }
    }
}