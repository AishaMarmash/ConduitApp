using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.Services;
using Conduit.Domain.ViewModels.RequestBody;

namespace Conduit.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUsersService _userService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public UsersController(IUsersService userService, IMapper mapper, IJwtService jwtService)
        {
            _userService = userService;
            _mapper = mapper;
            _jwtService = jwtService;
        }
        [HttpPost]
        public IActionResult Register([FromBody] RegisterModel registerModel)
        {
            if (_userService.UserExist(registerModel.User.Email))
            {
                return Conflict("email has already been taken");
            }
            if (_userService.UserExist(null,registerModel.User.Username))
            {
                return Conflict("username has already been taken");
            }
            User user = _mapper.Map<User>(registerModel.User);
            _userService.RegisterUser(user);
            
            var token = _jwtService.GenerateSecurityToken(registerModel.User.Email);
            var response = _userService.PrepareUserResponse(user, token);
            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (!_userService.UserExist(loginModel.User.Email))
            {
                return NotFound("user is not exist");
            }
            var userFromRequest = _mapper.Map<User>(loginModel.User);
            var userFromRepo = _userService.LoginUser(userFromRequest);
            if (userFromRepo == null)
            {
                return Forbid("Password is wrong");
            }
            else
            {
                var token = _jwtService.GenerateSecurityToken(loginModel.User.Email);
                var response = _userService.PrepareUserResponse(userFromRepo, token);
                return Ok(response);
            }
        }
    }
}