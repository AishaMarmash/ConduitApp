using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.ViewModels;
using Conduit.Domain.Services;

namespace Conduit.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper, IJwtService jwtService)
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
                return Conflict("user already exist");
            }
            User user = _mapper.Map<User>(registerModel.User);
            _userService.Add(user);
            var userResponse = _mapper.Map<UserForResponse>(user);
            userResponse.Token = _jwtService.ExtractToken(registerModel.User.Email);
            UserResponse response = new();
            response.user = userResponse;
            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult login([FromBody] LoginModel loginModel)
        {
            if (!_userService.UserExist(loginModel.User.Email))
            {
                return NotFound("user is not exist");
            }
            var user = _mapper.Map<User>(loginModel.User);
            var result = _userService.Find(user);
            if (result == null)
            {
                throw new Exception("Password is uncorrect");
            }
            else
            {
                UserForResponse userResponse = _mapper.Map<UserForResponse>(result);
                userResponse.Token = _jwtService.ExtractToken(loginModel.User.Email);
                UserResponse response = new();
                response.user = userResponse;
                return Ok(response);
            }
        }
    }
}