using Conduit.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Controllers
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfilesController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IUsersService _usersService;

        public ProfilesController(IProfileService profileService, IUsersService usersService)
        {
            _profileService = profileService;
            _usersService = usersService;
        }
        [HttpGet("{username}")]
        public ActionResult GetProfile(string username)
        {
            var user = _profileService.GetProfile(username);
            if (user == null)
            {
                return NotFound("user not found");
            }
            else
            {
                var response = _profileService.PrepareProfileResponse(user);
                bool isAuthenticated = _usersService.CheckAuthentication();
                if (isAuthenticated)
                {
                    response = _profileService.ApplyFollowingStatus(response);
                }
                return Ok(response);
            }
        }

        [HttpPost("{username}/follow")]
        [Authorize]
        public IActionResult FollowUser(string username)
        {
            var userEmail = _usersService.GetCurrentUserEmail();
            var currentUser = _usersService.GetUserByEmail(userEmail);
            var followedUser = _usersService.GetUserByName(username);
            if(followedUser==null)
            {
                return NotFound("user not found to follow");
            }
            else if(currentUser!=null)
            {
                _profileService.FollowUser(currentUser, followedUser);
                var response = _profileService.GetFollowingActivityResponse(currentUser, followedUser);
                return Ok(response);
            }
            return Unauthorized();
        }

        [HttpDelete("{username}/follow")]
        [Authorize]
        public IActionResult UnFollowUser(string username)
        {
            var userEmail = _usersService.GetCurrentUserEmail();
            var currentUser = _usersService.GetUserByEmail(userEmail);
            var unFollowedUser = _usersService.GetUserByName(username);
            if (unFollowedUser == null)
            {
                return NotFound("user not found to unfollow");
            }
            else if(currentUser!=null)
            {
                _profileService.UnFollowUser(currentUser, unFollowedUser);
                var response = _profileService.GetFollowingActivityResponse(currentUser, unFollowedUser);
                return Ok(response);
            }
            return Unauthorized();
        }
    }
}