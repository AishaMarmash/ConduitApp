using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.Repositories;
using Conduit.Domain.Services;
using Conduit.Domain.ViewModels;

namespace Conduit.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentsRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IProfileService _profileService;
        private readonly IUsersService _usersService;
        public CommentService(ICommentsRepository commentRepository, IMapper mapper , IProfileService profileService, IUsersService usersService)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _profileService = profileService;
            _usersService = usersService;
        }
        public Comment AddComment(string slug, Comment comment, User currentUser)
        {
            return _commentRepository.AddComment(slug, comment, currentUser);
        }
        public List<Comment> GetComments(string slug)
        {
            return _commentRepository.GetComments(slug);
        }
        public void DeleteComment(Comment comment)
        {
            _commentRepository.DeleteComment(comment);
        }
        public CommentResponse BuildResponse(Comment comment)
        {
            CommentResponse response = new();
            response.Comment = _mapper.Map<CommentResponseDto>(comment);
            return response;
        }
        public ListCommentResponse BuildResponse(List<Comment> comments)
        {
            ListCommentResponse response = new();
            List<CommentResponseDto> commentsList = new();
            foreach (var comment in comments)
            {
                var mappedComment = _mapper.Map<CommentResponseDto>(comment);
                mappedComment.Author = _mapper.Map<ProfileResponseDto>(comment.Author);
                bool isAuthenticated = _usersService.CheckAuthentication();
                if(isAuthenticated)
                {
                    mappedComment.Author = _profileService.ApplyFollowingStatus(mappedComment.Author);
                }
                commentsList.Add(mappedComment);
            }
            response.Comments = commentsList;
            return response;
        }
    }
}