using Conduit.Domain.Entities;
using Conduit.Domain.ViewModels;

namespace Conduit.Domain.Services
{
    public interface IArticleService
    {
        public Article AddArticle(Article article,string email);
        public Article FindArticle(string slug);
        public void DeleteArticle(string slug ,string authorEmail);
        public void UpdateArticle(Article articleToSave, string authorEmail);
        public List<Article> ListArticles(int limit, int offset);
        public List<Article> ListArticlesByTag(string tag,int limit,int offset);
        public List<Article> ListArticlesByAuthor(string tag,  int limit,int  offset);
        public List<Article> ListArticlesByFavorited(string favorited, int limit, int offset);
        public List<Article> FeedArticles(User currentUser, int limit, int offset);
        public void FavoriteArticle(User currentUser, Article favoritedArticle);
        public void UnFavoriteArticle(User currentUser, Article unFavoritedArticle);
        public List<string> GetTags();
        public Article PrepareArticleToSave(CreateArticleDto articleDto);
        public ArticleResponse BuildResponse(Article newArticle);
        public ListArticleResponse BuildResponse(List<Article> articles);
    }
}