using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.Services;
using Conduit.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Controllers
{
    [Route("api/articles/{slug}/comments")]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public CommentsController(ICommentService commentService, IMapper mapper ,IUsersService usersService)
        {
            _commentService = commentService;
            _mapper = mapper;
            _usersService = usersService;
        }
        [HttpPost]
        [Authorize]
        public IActionResult AddComment(string slug, [FromBody] CommentModel recievedModel)
        {
            var currentUserEmail = _usersService.GetCurrentUserEmail();
            var currentUser = _usersService.GetUserByEmail(currentUserEmail);
            var comment = _mapper.Map<Comment>(recievedModel.Comment);
            if(currentUser!=null)
            {
                var addedComment = _commentService.AddComment(slug, comment, currentUser);
                var response = _commentService.BuildResponse(addedComment);
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult GetComment(string slug)
        {
            List<Comment> comments = _commentService.GetComments(slug);
            var commentsListResponse = _commentService.BuildResponse(comments);
            return Ok(commentsListResponse);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteComment(string slug, int id)
        {
            List<Comment> comments = _commentService.GetComments(slug);
            var deletedComment = comments.FirstOrDefault(u => u.Id == id);
            if(deletedComment!=null)
            {
                _commentService.DeleteComment(deletedComment);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}