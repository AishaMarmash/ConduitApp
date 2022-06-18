using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Data.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        protected readonly AppContext _context;

        public CommentsRepository(AppContext context)
        {
            _context = context;
        }
        public Comment AddComment(string slug, Comment comment, User currentUser)
        {
            _context.Articles.First(a => a.Slug == slug).Comments.Add(comment);
            _context.Users.First(a => a.Id == currentUser.Id).Comments.Add(comment);
            _context.SaveChanges();
            return comment;
        }
        public List<Comment> GetComments(string slug)
        {
            var comments = _context.Articles.Include(a => a.Comments).ThenInclude(a => a.Author).First(a => a.Slug == slug).Comments;
            return comments;
        }
        public void DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }
    }
}