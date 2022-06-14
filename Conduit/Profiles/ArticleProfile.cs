using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.ViewModels.RequestBody;
using Conduit.Services;

namespace Conduit.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<CreateArticleDto, Article>()
                .ForMember(
                dest => dest.TagList,
                opt => opt.MapFrom(src => src.Tags.Combine()));
            CreateMap<UpdateArticleDto, Article>()
                 .ForMember(
                 dest => dest.Slug,
                 opt => opt.MapFrom(src => src.Title.GenerateSlug()))
                 .ForMember(dest => dest.UpdatedAt,
                 opt => opt.MapFrom(src => DateTime.Now))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}