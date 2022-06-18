using Conduit.Domain.Entities;

namespace Conduit.Domain.Repositories
{
    public interface ICommentsRepository
    {
        public Comment AddComment(string slug, Comment comment, User currentUser);
        public List<Comment> GetComments(string slug);
        public void DeleteComment(Comment comment);
    }
}