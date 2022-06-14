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

    }
}