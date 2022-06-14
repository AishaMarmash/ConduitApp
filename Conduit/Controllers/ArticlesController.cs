using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.Services;
using Conduit.Domain.ViewModels;
using Conduit.Domain.ViewModels.RequestBody;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Conduit.Controllers
{
    [Route("api/articles")]
    [ApiController]
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        IUsersService _usersService;

        public ArticlesController(IArticleService articleService, IMapper mapper, IJwtService jwtService, IUsersService usersService)
        {
            _articleService = articleService;
            _mapper = mapper;
            _jwtService = jwtService;
            _usersService = usersService;
        }
        [Authorize]
        [HttpPost]
        public ActionResult CreateArticle([FromBody] CreateArticleModel recievedModel)
        {
            CreateArticleDto recievedArticle = recievedModel.Article;
            string authorEmail = _jwtService.GetEmailClaim();
            var newArticle = _articleService.PrepareArticleToSave(recievedArticle);
            _articleService.Add(newArticle, authorEmail);
            return Ok(newArticle);
        }
        [Authorize]
        [HttpPut("{slug}")]
        public ActionResult UpdateArticle(string slug, [FromBody] UpdateArticleModel recievedModel)
        {
            var articleFromRepo = _articleService.Find(slug);
            UpdateArticleDto recievedArticle = recievedModel.Article;
            _mapper.Map(recievedArticle, articleFromRepo);
            string userEmail = _jwtService.GetEmailClaim();
            if (articleFromRepo.User.Email == userEmail)
            {
                _articleService.Update(articleFromRepo, userEmail);
            }
            return Ok();
        }
        [HttpGet("{slug}")]
        public ActionResult ReadArticle(string slug)
        {
            var article = _articleService.Find(slug);
            return Ok(article);
        }
        [Authorize]
        [HttpDelete("{slug}")]
        public ActionResult DeleteArticle(string slug)
        {
            string authorEmail = _jwtService.GetEmailClaim();
            _articleService.Delete(slug, authorEmail);
            return Ok();
        }

        [HttpGet]
        public ActionResult ListArticles([FromQuery] string? tag = null, [FromQuery] string? author = null, [FromQuery] string? favorited = null, [FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            if (tag != null)
            {
                var articles = _articleService.ListArticlesByTag(tag, limit, offset);
                return Ok(articles);
            }
            if (author != null)
            {
                var articles = _articleService.ListArticlesByAuthor(author, limit, offset);
                return Ok(articles);
            }
            if (favorited != null)
            {
                var articles = _articleService.ListArticlesByFavorited(favorited, limit, offset);
                return Ok(articles);
            }
            else
            {
                var articles = _articleService.ListArticles(limit, offset);
                return Ok(articles);
            }
            return NotFound();
        }
        [HttpGet("feed")]
        [Authorize]
        public ActionResult FeedArticles([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            var tokenString = _jwtService.GetCurrentAsync();
            var tokenJwt = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var emailClaim = tokenJwt.Claims.First(c => c.Type == "email").Value;
            var currentUser = _usersService.FindByEmail(emailClaim);

            var articles = _articleService.FeedArticles(currentUser, limit, offset);
            return Ok(articles);
        }
        [HttpPost("{slug}/favorite")]
        [Authorize]
        public IActionResult FavoriteArticle(string slug)
        {
            var tokenString = _jwtService.GetCurrentAsync();
            var tokenJwt = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var emailClaim = tokenJwt.Claims.First(c => c.Type == "email").Value;
            var currentUser = _usersService.FindByEmail(emailClaim);
            var favoritedArticle = _articleService.Find(slug);
            _articleService.FavoriteArticle(currentUser, favoritedArticle);
            return Ok(favoritedArticle);
        }
        [HttpDelete("{slug}/favorite")]
        [Authorize]
        public IActionResult UnFavoriteArticle(string slug)
        {
            var tokenString = _jwtService.GetCurrentAsync();
            var tokenJwt = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var emailClaim = tokenJwt.Claims.First(c => c.Type == "email").Value;
            var currentUser = _usersService.FindByEmail(emailClaim);
            var unFavoritedArticle = _articleService.Find(slug);
            _articleService.UnFavoriteArticle(currentUser, unFavoritedArticle);
            return Ok(unFavoritedArticle);
        }
        [HttpPost("{slug}/comments")]
        [Authorize]
        public IActionResult AddComment(string slug, [FromBody] CommentModel recievedModel)
        {
            var tokenString = _jwtService.GetCurrentAsync();
            var tokenJwt = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var emailClaim = tokenJwt.Claims.First(c => c.Type == "email").Value;
            var currentUser = _usersService.FindByEmail(emailClaim);
            var comment = recievedModel.Comment;
            var comentToSend = _mapper.Map<Comment>(comment);
            comentToSend.CreatedAt = comentToSend.UpdatedAt = DateTime.UtcNow;
            var finalComment = _articleService.AddComment(slug, comentToSend, currentUser);
            return Ok(finalComment);
        }
        [HttpGet("{slug}/comment")]
        public IActionResult GetComment(string slug)
        {
            List<Comment> comments = _articleService.GetComments(slug);
            return Ok(comments);
        }
        [HttpDelete("{slug}/comments/{id}")]
        [Authorize]
        public IActionResult DeleteComment(string slug,int id)
        {
            List<Comment> comments = _articleService.GetComments(slug);
            _articleService.DeleteComment(comments.First(u => u.Id == id));
            return Ok();
        }
    }
}