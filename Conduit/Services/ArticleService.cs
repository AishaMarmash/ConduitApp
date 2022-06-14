using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.Repositories;
using Conduit.Domain.Services;
using Conduit.Domain.ViewModels.RequestBody;

namespace Conduit.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private IMapper _mapper;

        public ArticleService(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }
        public void Add(Article article, string authorEmail)
        {
            _articleRepository.Add(article, authorEmail);
        }
        public Article Find(string slug)
        {
            return _articleRepository.Find(slug);
        }
        public void Delete(string slug, string authorEmail)
        {
            _articleRepository.Delete(slug,authorEmail);
        }

        public Article PrepareArticleToSave(CreateArticleDto articleDto)
        {
            var newArticle = _mapper.Map<Article>(articleDto);
            newArticle.Slug = newArticle.Title.GenerateSlug();
            newArticle.Favorited = false;
            newArticle.FavoritesCount = 0;
            newArticle.CreatedAt = newArticle.UpdatedAt = DateTime.Now;
            return newArticle;
        }

        public void Update(Article articleToSave, string authorEmail)
        {
            _articleRepository.Update(articleToSave, authorEmail);
        }
        public List<Article> ListArticles(int limit, int offset)
        {
            var articles = _articleRepository.ListArticles(limit, offset);
            return articles;
        }

        public List<Article> ListArticlesByTag(string tag,int limit, int offset)
        {
            var articles = _articleRepository.ListArticlesByTag(tag,limit,offset);
            return articles;
        }

        public List<Article> ListArticlesByAuthor(string authorName,int limit,int offset)
        {
            var articles = _articleRepository.ListArticlesByAuthor(authorName,limit,offset);
            return articles;
        }
    }
}