using AutoMapper;
using Conduit.Data;
using Conduit.Data.Repositories;
using Conduit.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;

namespace Conduit.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class userController : Controller
    {
        Data.AppContext userContext;
        private IConfiguration _config;
        UserRepository userRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public userController(IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            userContext = new();
            _config = config;
            userRepo = new UserRepository(userContext);
            _config = config;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var token = GetCurrentAsync();
            var tokenn = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var claim = tokenn.Claims.First(c => c.Type == "email").Value;
            var result = userRepo.Find(claim);
            UserForResponse userResponse = _mapper.Map<UserForResponse>(result);
            userResponse.Token = token;
            return Ok(userResponse);
        }
        [Authorize]
        [HttpPut]
        public ActionResult Updateuser(UserForUpdateDto userForUpdateDto)
        {
            var token = GetCurrentAsync();
            var tokenn = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var claim = tokenn.Claims.First(c => c.Type == "email").Value;
            var userFromRepo = userRepo.Find(claim);
            if (userFromRepo == null)
            {
                return NotFound();
            }
            userRepo.UpdateUser(userFromRepo, userForUpdateDto);
            return NoContent();
        }
        [NonAction]
        private string GetCurrentAsync()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }
    }
}