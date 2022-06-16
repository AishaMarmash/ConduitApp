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
            CreateMap<CreateArticleDto, Article>()
                .ForMember(dest => dest.CreatedAt,
                 opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdatedAt,
                 opt => opt.MapFrom(src => DateTime.Now));
            
            CreateMap<UpdateArticleDto, Article>()
                 .ForMember(
                 dest => dest.Slug,
                 opt => opt.MapFrom(src => src.Title.GenerateSlug()))
                 .ForMember(dest => dest.UpdatedAt,
                 opt => opt.MapFrom(src => DateTime.Now))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            
            CreateMap<CommentDto, Comment>()
                .ForMember(dest => dest.UpdatedAt,
                 opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.CreatedAt,
                 opt => opt.MapFrom(src => DateTime.Now))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Article, ArticleResponseDto>()
                 .ForMember(
                 dest => dest.TagList,
                 opt => opt.MapFrom(src => (!String.IsNullOrEmpty(src.TagList)) ?(src.TagList.MoveToList()): null));
            CreateMap<CreateArticleDto, Article>();


        }
    }
}