using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.Services;
using Conduit.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Controllers
{
    [Route("api/articles")]
    [ApiController]
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public ArticlesController(IArticleService articleService, IMapper mapper,IUsersService usersService)
        {
            _articleService = articleService;
            _mapper = mapper;
            _usersService = usersService;
        }
        [Authorize]
        [HttpPost]
        public ActionResult CreateArticle([FromBody] CreateArticleModel recievedModel)
        {
            CreateArticleDto recievedArticle = recievedModel.Article;
            string authorEmail = _usersService.GetCurrentUserEmail();
            var newArticle = _articleService.PrepareArticleToSave(recievedArticle);
            Article addedArticle = _articleService.AddArticle(newArticle, authorEmail);
            var response = _articleService.BuildResponse(addedArticle);
            return Ok(response);
        }
        [Authorize]
        [HttpPut("{slug}")]
        public ActionResult UpdateArticle(string slug, [FromBody] UpdateArticleModel recievedModel)
        {
            var articleFromRepo = _articleService.FindArticle(slug);
            UpdateArticleDto recievedArticle = recievedModel.Article;
            var updatedArticle = _mapper.Map(recievedArticle, articleFromRepo);
            string userEmail = _usersService.GetCurrentUserEmail();
            if (articleFromRepo.User.Email == userEmail)
            {
                _articleService.UpdateArticle(articleFromRepo, userEmail);
            }
            else
            {
                return Unauthorized();
            }
            var response = _articleService.BuildResponse(updatedArticle);
            return Ok(response);
        }
        [Authorize]
        [HttpDelete("{slug}")]
        public ActionResult DeleteArticle(string slug)
        {
            string userEmail = _usersService.GetCurrentUserEmail();
            var articleFromRepo = _articleService.FindArticle(slug);
            if(userEmail == articleFromRepo.User.Email)
            {
                if(articleFromRepo!=null)
                {
                    _articleService.DeleteArticle(slug, userEmail);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
                return Unauthorized();
        }

        [HttpGet("{slug}")]
        public ActionResult ReadArticle(string slug)
        {
            var article = _articleService.FindArticle(slug);
            if(article != null)
            {
                var response = _articleService.BuildResponse(article);
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ListArticles([FromQuery] string? tag = null, [FromQuery] string? author = null, [FromQuery] string? favorited = null, [FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            List<Article> articles;
            if (tag != null)
            {
                articles = _articleService.ListArticlesByTag(tag, limit, offset);
            }
            else if (author != null)
            {
                articles = _articleService.ListArticlesByAuthor(author, limit, offset);
            }
            else if (favorited != null)
            {
                articles = _articleService.ListArticlesByFavorited(favorited, limit, offset);
            }
            else
            {
                articles = _articleService.ListArticles(limit, offset);
            }
            var articlesListResponse = _articleService.BuildResponse(articles);
            return Ok(articlesListResponse);
        }
        [HttpGet("feed")]
        [Authorize]
        public ActionResult FeedArticles([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            var currentUserEmail = _usersService.GetCurrentUserEmail();
            var currentUser = _usersService.GetUserByEmail(currentUserEmail);
            if(currentUser!=null)
            {
                var articles = _articleService.FeedArticles(currentUser, limit, offset);
                var articlesListResponse = _articleService.BuildResponse(articles);
                return Ok(articlesListResponse);
            }
            else
            {
                return Unauthorized();
            }
        }
        [HttpPost("{slug}/favorite")]
        [Authorize]
        public IActionResult FavoriteArticle(string slug)
        {
            var currentUserEmail = _usersService.GetCurrentUserEmail();
            var currentUser = _usersService.GetUserByEmail(currentUserEmail);
            var favoritedArticle = _articleService.FindArticle(slug);
            if(favoritedArticle != null && currentUser != null)
            {
                _articleService.FavoriteArticle(currentUser, favoritedArticle);
                var response = _articleService.BuildResponse(favoritedArticle);
                return Ok(response);
            }
            else 
            { 
                return NotFound(); 
            }
        }
        [HttpDelete("{slug}/favorite")]
        [Authorize]
        public IActionResult UnFavoriteArticle(string slug)
        {
            var currentUserEmail = _usersService.GetCurrentUserEmail();
            var currentUser = _usersService.GetUserByEmail(currentUserEmail);
            var unFavoritedArticle = _articleService.FindArticle(slug);
            if(unFavoritedArticle != null && currentUser != null)
            {
                _articleService.UnFavoriteArticle(currentUser, unFavoritedArticle);
                var response = _articleService.BuildResponse(unFavoritedArticle);
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }
    }
}