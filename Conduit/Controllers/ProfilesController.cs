using AutoMapper;
using Conduit.Data;
using Conduit.Data.Repositories;
using Conduit.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Controllers
{
    [ApiController]
    [Route("api/Profiles")]
    public class ProfilesController : Controller
    {
        UserContext _userContext;
        ProfileRepository profileRepo;
        private readonly IMapper _mapper;

        public ProfilesController(IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userContext = new();
            profileRepo = new ProfileRepository(_userContext);
            _mapper = mapper;
        }

        [HttpGet("{username}")]
        public ActionResult Index(string username)
        {
            User user = profileRepo.GetUser(username);
            if(user == null)
            {
                return NotFound();
            }
            else
            {
                ProfileForResponse profile = _mapper.Map<ProfileForResponse>(user);
                return Ok(profile);
            }
        }
    }
}
