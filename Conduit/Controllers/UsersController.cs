using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.Services;
using Conduit.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Conduit.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUsersService _userService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly ITokenManager _tokenManager;
        public UsersController(IUsersService userService, IMapper mapper, IJwtService jwtService, ITokenManager tokenManager)
        {
            _userService = userService;
            _mapper = mapper;
            _jwtService = jwtService;
            _tokenManager = tokenManager;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
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
            await _userService.RegisterUser(user);
            
            var token = _jwtService.GenerateSecurityToken(registerModel.User.Email);
            var response = _userService.PrepareUserResponse(user, token);
            return Ok(response);
        }
        [HttpPost("login")]
        [AllowAnonymous]
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
        [HttpPost("logout")]
        public async Task<IActionResult> CancelAccessToken()
        {
            await _tokenManager.DeactivateCurrentAsync();
            return Ok();
        }
    }
}