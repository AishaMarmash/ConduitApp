using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.Repositories;
using Conduit.Domain.Services;
using Conduit.Domain.ViewModels;

namespace Conduit.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;
        private readonly IProfileService _profileService;
        public ArticleService(IArticleRepository articleRepository,
                                IMapper mapper,
                                IUsersService usersService,
                                IProfileService profileService)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _usersService = usersService;
            _profileService = profileService;
        }
        public Article AddArticle(Article article, string authorEmail)
        {
            return _articleRepository.AddArticle(article, authorEmail);
        }
        public Article FindArticle(string slug)
        {
            return _articleRepository.FindArticle(slug);
        }
        public void DeleteArticle(string slug, string authorEmail)
        {
            _articleRepository.DeleteArticle(slug,authorEmail);
        }
        public void UpdateArticle(Article articleToSave, string authorEmail)
        {
            _articleRepository.UpdateArticle(articleToSave, authorEmail);
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
        public List<Article> ListArticlesByFavorited(string favorited, int limit, int offset)
        {
            var articles = _articleRepository.ListArticlesByFavorited(favorited, limit, offset);
            return articles;
        }
        public List<Article> FeedArticles(User currentUser, int limit, int offset)
        {
            var articles = _articleRepository.FeedArticles(currentUser, limit, offset);
            return articles;
        }
        public void FavoriteArticle(User currentUser, Article favoritedArticle)
        {
            _articleRepository.FavoriteArticle(currentUser, favoritedArticle);
        }
        public void UnFavoriteArticle(User currentUser, Article unFavoritedArticle)
        {
            _articleRepository.UnFavoriteArticle(currentUser, unFavoritedArticle);
        }
        public List<string> GetTags()
        {
            return _articleRepository.GetTags();
        }
        public Article PrepareArticleToSave(CreateArticleDto articleDto)
        {
            var newArticle = _mapper.Map<Article>(articleDto);
            if(articleDto.TagList != null)
            {
                newArticle.TagList = articleDto.TagList.Combine();
            }
            newArticle.Slug = newArticle.Title.GenerateSlug();
            newArticle.CreatedAt = newArticle.UpdatedAt = DateTime.Now;
            return newArticle;
        }
        public ArticleResponse BuildResponse(Article newArticle)
        {
            ArticleResponseDto article = _mapper.Map<ArticleResponseDto>(newArticle);
            _mapper.Map(newArticle.User, article.Author);
            bool isAuthenticated = _usersService.CheckAuthentication();
            if (isAuthenticated)
            {
                article.Author = _profileService.ApplyFollowingStatus(article.Author);
            }
            ArticleResponse response = new();
            response.Article = article;
            return response;
        }
        public ListArticleResponse BuildResponse(List<Article> articles)
        {
            ListArticleResponse response = new();
            List<ArticleResponseDto> articlesList = new();
            foreach (var article in articles)
            {
                var mappedArticle = _mapper.Map<ArticleResponseDto>(article);
                mappedArticle.Author = _mapper.Map<ProfileResponseDto>(article.User);
                bool isAuthenticated = _usersService.CheckAuthentication();
                if (isAuthenticated)
                {
                    mappedArticle.Author = _profileService.ApplyFollowingStatus(mappedArticle.Author);
                }
                articlesList.Add(mappedArticle);
            }
            response.Articles = articlesList;
            response.ArticlesCount = articlesList.Count;
            return response;
        }
    }
}