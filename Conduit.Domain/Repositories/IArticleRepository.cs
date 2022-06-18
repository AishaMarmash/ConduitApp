using Conduit.Domain.Entities;
namespace Conduit.Domain.Repositories
{
    public interface IArticleRepository
    {
        public Article AddArticle(Article article, string authorEmail);
        public Article FindArticle(string slug);
        public void DeleteArticle(string slug, string authorEmail);
        void UpdateArticle(Article articleToSave, string authorEmail);
        public List<Article> ListArticles(int limit, int offset);
        public List<Article> ListArticlesByTag(string tag, int limit,int offset);
        public List<Article> ListArticlesByAuthor(string authorName,int limit,int offset);
        public List<Article> ListArticlesByFavorited(string authorName, int limit, int offset);
        public List<Article> FeedArticles(User currentUser, int limit, int offset);
        public void FavoriteArticle(User currentUser, Article favoritedArticle);
        public void UnFavoriteArticle(User currentUser, Article unFavoritedArticle);
        public List<string> GetTags();
    }
}