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
    }
}