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

        public ProfilesController(IProfileService profileService,IJwtService jwtService, IUsersService usersService)
        {
            _profileService = profileService;
            _jwtService = jwtService;
            _usersService = usersService;
        }

        [HttpGet("{username}")]
        public ActionResult GetProfiles(string username)
        {
            var user = _profileService.GetProfile(username);
            if (user == null)
            {
                return NotFound("user not found");
            }
            else
            {
                var response = _profileService.PrepareProfileResponse(user);
                var tokenString = _jwtService.GetCurrentAsync();
                if (!string.IsNullOrEmpty(tokenString))
                {
                    var userEmail = _usersService.GetCurrentUserEmail();
                    var currentUser = _usersService.FindByEmail(userEmail);
                    var otherUser = _usersService.FindByUsername(username);
                    response.Profile.Following = _profileService.FollowingStatus(currentUser, otherUser);
                }
                return Ok(response);
            }
        }

        [HttpPost("{username}/follow")]
        [Authorize]
        public IActionResult FollowUser(string username)
        {
            var userEmail = _usersService.GetCurrentUserEmail();
            var currentUser = _usersService.FindByEmail(userEmail);
            var followedUser = _usersService.FindByUsername(username);
            if(followedUser==null)
            {
                return NotFound("user not found to follow");
            }
            else if(currentUser!=null)
            {
                _profileService.FollowUser(currentUser, followedUser);
                var response = GetFollowingActivityResponse(currentUser, followedUser);
                return Ok(response);
            }
            return Unauthorized();
        }

        [HttpDelete("{username}/follow")]
        [Authorize]
        public IActionResult UnFollowUser(string username)
        {
            var userEmail = _usersService.GetCurrentUserEmail();
            var currentUser = _usersService.FindByEmail(userEmail);
            var unFollowedUser = _usersService.FindByUsername(username);
            if (unFollowedUser == null)
            {
                return NotFound("user not found to unfollow");
            }
            else if(currentUser!=null)
            {
                _profileService.UnFollowUser(currentUser, unFollowedUser);
                var response = GetFollowingActivityResponse(currentUser, unFollowedUser);
                return Ok(response);
            }
            return Unauthorized();
        }
        [NonAction]
        public ProfileResponse GetFollowingActivityResponse(User currentUser,User neededUser)
        {
            var user = _profileService.GetProfile(neededUser.Username);
            var response = _profileService.PrepareProfileResponse(user);
            response.Profile.Following = _profileService.FollowingStatus(currentUser, neededUser);
            return response;
        }

    }
}