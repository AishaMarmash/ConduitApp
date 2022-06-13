using AutoMapper;
using Conduit.Data;
using Conduit.Data.Repositories;
using Conduit.Domain.Entities;
using Conduit.Domain.Services;
using Conduit.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Controllers
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfilesController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IMapper _mapper;

        public ProfilesController(IProfileService profileService, IMapper mapper)
        {
            _profileService = profileService;
            _mapper = mapper;
        }

        [HttpGet("{username}")]
        public ActionResult GetProfiles(string username)
        {
            var user = _profileService.GetProfile(username);
            if(user == null)
            {
                return NotFound();
            }
            else
            {
                var response = _profileService.PrepareProfileResponse(user);
                return Ok(response);
            }
        }
    }
}