using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.ViewModels;

namespace Conduit.Profiles
{
    public class CommentProfile :Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentResponseDto>()
                .ForMember(dest => dest.CreatedAt,
                            opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdatedAt,
                            opt => opt.MapFrom(src => DateTime.Now));

        }
    }
}
