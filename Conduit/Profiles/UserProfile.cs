using AutoMapper;
using Conduit.Domain.Models;

namespace Conduit.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserForResponse>();
            CreateMap<User, UserForUpdateDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<User, ProfileForResponse>();
        }
    }
}