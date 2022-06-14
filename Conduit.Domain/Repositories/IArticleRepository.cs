using Conduit.Domain.Entities;
namespace Conduit.Domain.Repositories
{
    public interface IArticleRepository
    {
        public void Add(Article article, string authorEmail);
        public Article Find(string slug);
        public void Delete(string slug, string authorEmail);
        void Update(Article articleToSave, string authorEmail);
        public List<Article> ListArticles(int limit, int offset);
        public List<Article> ListArticlesByTag(string tag, int limit,int offset);
        public List<Article> ListArticlesByAuthor(string authorName,int limit,int offset);
        public List<Article> ListArticlesByFavorited(string authorName, int limit, int offset);
        public List<Article> FeedArticles(User currentUser, int limit, int offset);
        public void FavoriteArticle(User currentUser, Article favoritedArticle);
        public void UnFavoriteArticle(User currentUser, Article unFavoritedArticle);
        public Comment AddComment(string slug, Comment comment, User currentUser);
        public List<Comment> GetComments(string slug);
        public void DeleteComment(Comment comment);
        public List<string> GetTags();
    }
}