using Conduit.Domain.Entities;
using Conduit.Domain.ViewModels.RequestBody;

namespace Conduit.Domain.Services
{
    public interface IArticleService
    {
        public void Add(Article article,string email);
        public Article Find(string slug);
        public void Delete(string slug ,string authorEmail);
        public void Update(Article articleToSave, string authorEmail);
        public Article PrepareArticleToSave(CreateArticleDto articleDto);
        public List<Article> ListArticles(int limit, int offset);
        public List<Article> ListArticlesByTag(string tag,int limit,int offset);
        public List<Article> ListArticlesByAuthor(string tag,  int limit,int  offset);
        public List<Article> ListArticlesByFavorited(string favorited, int limit, int offset);
        public List<Article> FeedArticles(User currentUser, int limit, int offset);
        public void FavoriteArticle(User currentUser, Article favoritedArticle);
        public void UnFavoriteArticle(User currentUser, Article unFavoritedArticle);
        public Comment AddComment(string slug,Comment comment,User currentUser);
        public List<Comment> GetComments(string slug);
        public void DeleteComment(Comment comment);
        public List<string> GetTags();
    }
}