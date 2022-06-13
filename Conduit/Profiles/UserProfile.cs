using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.ViewModels;

namespace Conduit.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<LoginUserDto, User>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<User, UserForResponse>();
            CreateMap<User, UserForUpdateDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<User, ProfileForResponse>();
        }
    }
}