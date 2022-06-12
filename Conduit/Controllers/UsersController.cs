using Conduit.Data;
using Conduit.Data.Repositories;
using Conduit.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Conduit.Services;
using Microsoft.Extensions.Caching.Distributed;
using AutoMapper;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;

namespace Conduit.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : Controller
    {
        UserContext userContext;
        private IConfiguration _config;
        UserRepository userRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            userContext = new();
            _config = config;
            userRepo = new UserRepository(userContext);
            _config = config;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost]
        public IActionResult Index([FromBody] RegisterModel registerModel)
        {
            if (registerModel.Username.Length == 0)
            {
                throw new Exception("Username Field is required");
            }
            if (registerModel.email.Length == 0)
            {
                throw new Exception("Email Field is required");
            }
            if (registerModel.Password.Length == 0)
            {
                throw new Exception("Password Field is required");
            }
            if (userRepo.UserExist(registerModel.email))
            {
                throw new Exception("user is exist");
            }
            var user = new User()
            {
                Username = registerModel.Username,
                Password = registerModel.Password,
                Email = registerModel.email
            };
            userRepo.Add(user);
            var userResponse = _mapper.Map<UserForResponse>(user);
            var token = GetToken(user.Email);
            userResponse.Token = token;
            return Ok(userResponse);
        }
        [HttpPost("login")]
        public ActionResult<UserForResponse> login([FromBody] LoginModel loginModel)
        {
            if (loginModel.Email.Length == 0)
            {
                throw new Exception("Email Field is required");
            }
            if (loginModel.Password.Length == 0)
            {
                throw new Exception("Password Field is required");
            }
            if (!userRepo.UserExist(loginModel.Email))
            {
                throw new Exception("user is not exist");
            }
            User user = new User()
            {
                Email = loginModel.Email,
                Password = loginModel.Password
            };
            var result = userRepo.Find(user);
            if (result == null)
            {
                throw new Exception("Password is uncorrect");
            }
            
            else
            {
                UserForResponse userResponse = _mapper.Map<UserForResponse>(result);
                var token = GetToken(user.Email);
                userResponse.Token = token;
                return Ok(userResponse);
            }
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
