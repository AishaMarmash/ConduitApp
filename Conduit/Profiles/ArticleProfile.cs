using AutoMapper;
using Conduit.Domain.Models;
using Conduit.Sercices;

namespace Conduit.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleForCreate, Article>()
                .ForMember(
                dest => dest.TagList,
                opt => opt.MapFrom(src => src.Tags.Combine()));
        }
    }
}