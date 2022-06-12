using AutoMapper;
using Conduit.Data;
using Conduit.Data.Repositories;
using Conduit.Domain.Models;
using Conduit.Domain.Services;
using Conduit.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Controllers
{
    [Route("api/article")]
    [ApiController]
    public class ArticlesController : Controller
    {
        Data.AppContext articleContext;
        private IConfiguration _config;
        ArticleRepository articleRepo;
        private readonly IMapper _mapper;

        public ArticlesController(IConfiguration config, IMapper mapper)
        {
            articleContext = new();
            _config = config;
            articleRepo = new ArticleRepository(articleContext);
            _config = config;
            _mapper = mapper;
        }
        [Authorize]
        [HttpPost]
        public ActionResult CreateArticle(ArticleForCreate article)
        {
            var articleToSave = _mapper.Map<Article>(article);
            articleToSave.Slug = articleToSave.Title.GenerateSlug();
            articleToSave.Favorited = false;
            articleToSave.FavoritesCount = 0;
            articleToSave.CreatedAt = articleToSave.UpdatedAt = DateTime.Now;
            articleRepo.Add(articleToSave);
            return Ok(articleToSave);
        }

        [HttpGet("{slug}")]
        public ActionResult ReadArticle(string slug)
        {
            var article = articleRepo.Find(slug);
            return Ok(article);
        }
        [Authorize]
        [HttpDelete("{slug}")]
        public ActionResult DeleteArticle(string slug)
        {
            var article = articleRepo.Find(slug);
            articleRepo.Delete(article);
            return Ok();
        }

        [NonAction]
        public string GetToken(string email)
        {
            var jwt = new JwtService(_config);
            var token = jwt.GenerateSecurityToken(email);
            return token;
        }

    }
}
