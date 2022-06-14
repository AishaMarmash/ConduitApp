using AutoMapper;
using Conduit.Domain.Services;
using Conduit.Domain.ViewModels.RequestBody;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Controllers
{
    [Route("api/article")]
    [ApiController]
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public ArticlesController(IArticleService articleService, IMapper mapper, IJwtService jwtService)
        {
            _articleService = articleService;
            _mapper = mapper;
            _jwtService = jwtService;
        }
        [Authorize]
        [HttpPost]
        public ActionResult CreateArticle([FromBody]CreateArticleModel CreateArticleModel)
        {
            string authorEmail = _jwtService.GetEmailClaim();
            var newArticle = _articleService.PrepareArticleToSave(CreateArticleModel.Article);
            _articleService.Add(newArticle, authorEmail);
            return Ok(newArticle);
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
            _articleService.Delete(slug,authorEmail);
            return Ok();
        }
        [Authorize]
        [HttpPut("{slug}")]
        public ActionResult UpdateArticle(string slug,[FromBody] UpdateArticleModel updateArticleModel )
        {
            var articleFromRepo = _articleService.Find(slug);
            _mapper.Map(updateArticleModel.Article,articleFromRepo);
            string authorEmail = _jwtService.GetEmailClaim();

            _articleService.Update(articleFromRepo, authorEmail);
            return Ok();
        }
        
        [HttpGet]
        public ActionResult ListArticles([FromQuery] string? tag = null , [FromQuery] string? author = null, [FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            if(tag != null)
            {
                var articles = _articleService.ListArticlesByTag(tag,limit,offset);
                return Ok(articles);
            }
            if(author != null)
            {
                var articles = _articleService.ListArticlesByAuthor(author, limit, offset);
                return Ok(articles);
            }
            else
            {
                var articles = _articleService.ListArticles(limit, offset);
                return Ok(articles);
            }
            return NotFound();
        }
    }
}