using AutoMapper;
using Conduit.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Controllers
{
    [ApiController]
    [Route("api/tags")]
    public class TagsController:Controller
    {
        private readonly IArticleService _articleService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public TagsController(IArticleService articleService, IMapper mapper, IJwtService jwtService)
        {
            _articleService = articleService;
            _mapper = mapper;
            _jwtService = jwtService;
        }
        [HttpGet]
        public IActionResult GetTags()
        {
            var response = _articleService.GetTags();
            return Ok(response);
        }

    }
}
