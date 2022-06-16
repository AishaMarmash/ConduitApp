using AutoMapper;
using Conduit.Domain.Services;
using Conduit.Domain.ViewModels.RequestBody;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Conduit.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUsersService _userService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public UserController(IUsersService userService, IMapper mapper, IJwtService jwtService)
        {
            _userService = userService;
            _mapper = mapper;
            _jwtService = jwtService;
        }
        [HttpGet]
        public ActionResult GetCurrentUser()
        {
            var tokenString = _jwtService.GetCurrentAsync();
            var tokenJwt = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var userEmail = tokenJwt.Claims.First(c => c.Type == "email").Value;
            
            var userFromRep = _userService.GetUserByEmail(userEmail);
            if(userFromRep != null)
            {
                var response = _userService.PrepareUserResponse(userFromRep, tokenString);
                return Ok(response);
            }
            else { return NotFound("user does not exist"); }
        }
        [HttpPut]
        public ActionResult Updateuser([FromBody] UpdateModel updateModel)
        {
            var tokenString = _jwtService.GetCurrentAsync();
            var tokenJwt = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var userEmail = tokenJwt.Claims.First(c => c.Type == "email").Value;

            var userFromRep = _userService.GetUserByEmail(userEmail);
            _mapper.Map(updateModel.User, userFromRep);
            _userService.SaveUserChanges();
            
            var response = _userService.PrepareUserResponse(userFromRep, tokenString);
            return Ok(response);
        }
    }
}