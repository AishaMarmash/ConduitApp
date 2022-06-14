using AutoMapper;
using Conduit.Data;
using Conduit.Data.Repositories;
using Conduit.Domain.Entities;
using Conduit.Domain.Services;
using Conduit.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Conduit.Controllers
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfilesController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IUsersService _usersService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public ProfilesController(IProfileService profileService, IMapper mapper, IJwtService jwtService, IUsersService usersService)
        {
            _profileService = profileService;
            _mapper = mapper;
            _jwtService = jwtService;
            _usersService = usersService;
        }

        [HttpGet("{username}")]
        public ActionResult GetProfiles(string username)
        {
            var user = _profileService.GetProfile(username);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var response = _profileService.PrepareProfileResponse(user);
                return Ok(response);
            }
        }
        [HttpPost("{username}/follow")]
        [Authorize]
        public IActionResult FollowUser(string username)
        {
            var tokenString = _jwtService.GetCurrentAsync();
            var tokenJwt = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var emailClaim = tokenJwt.Claims.First(c => c.Type == "email").Value;
            var currentUser = _usersService.FindByEmail(emailClaim);
            var followedUser = _usersService.FindByUsername(username);
            var result = _profileService.FollowUser(currentUser,followedUser);
            return Ok(result);
        }
        [HttpDelete("{username}/follow")]
        [Authorize]
        public IActionResult UnFollowUser(string username)
        {
            var tokenString = _jwtService.GetCurrentAsync();
            var tokenJwt = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var emailClaim = tokenJwt.Claims.First(c => c.Type == "email").Value;
            var currentUser = _usersService.FindByEmail(emailClaim);
            var followedUser = _usersService.FindByUsername(username);
            var result = _profileService.UnFollowUser(currentUser, followedUser);
            return Ok(result);
        }
    }
}