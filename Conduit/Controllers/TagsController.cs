using Conduit.Domain.Services;
using Conduit.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Controllers
{
    [ApiController]
    [Route("api/tags")]
    public class TagsController : Controller
    {
        private readonly IArticleService _articleService;

        public TagsController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        [HttpGet]
        public IActionResult GetTags()
        {
            var result = _articleService.GetTags();
            var response = new TagsResponse(result);
            return Ok(response);
        }
    }
}