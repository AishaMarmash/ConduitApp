using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.ViewModels;
using Conduit.Services;

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