using Conduit.Domain.Entities;
using Conduit.Domain.ViewModels;

namespace Conduit.Domain.Services
{
    public interface ICommentService
    {
        public Comment AddComment(string slug, Comment comment, User currentUser);
        public List<Comment> GetComments(string slug);
        public void DeleteComment(Comment comment);
        public CommentResponse BuildResponse(Comment comment);
        public ListCommentResponse BuildResponse(List<Comment> comments);
    }
}