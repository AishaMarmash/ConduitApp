﻿using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.Services;
using Conduit.Domain.ViewModels;
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
            var emailClaim = tokenJwt.Claims.First(c => c.Type == "email").Value;
            
            var userFromRep = _userService.FindByEmail(emailClaim);

            var response = _userService.PrepareUserResponse(userFromRep, tokenString);
            return Ok(response);
        }
        [HttpPut]
        public ActionResult Updateuser([FromBody] UpdateModel updateModel)
        {
            var tokenString = _jwtService.GetCurrentAsync();
            var tokenJwt = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var claim = tokenJwt.Claims.First(c => c.Type == "email").Value;

            var userFromRep = _userService.FindByEmail(claim);
            _mapper.Map(updateModel.User, userFromRep);
            _userService.UpdateUser(userFromRep);
            
            var response = _userService.PrepareUserResponse(userFromRep, tokenString);
            return Ok(response);
        }
    }
}