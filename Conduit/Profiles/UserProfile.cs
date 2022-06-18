using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.ViewModels;

namespace Conduit.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<LoginUserDto, User>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<User, UserResponseDto>();
            CreateMap<User, UpdateUserDto>();
            CreateMap<UpdateUserDto, User>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<User, ProfileResponseDto>();
            CreateMap<User, User>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}