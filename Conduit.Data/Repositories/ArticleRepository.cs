using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.Repositories;
using Conduit.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Data.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        protected readonly AppContext _context;
        IMapper _mapper;

        public ArticleRepository(AppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Add(Article article, string authorEmail)
        {
            User user = _context.Users.First(u => u.Email == authorEmail);
            user.Articles.Add(article);
            _context.SaveChanges();
        }

        public Article Find(string slug)
        {
            var article = _context.Articles.Include(a=>a.User).FirstOrDefault(ar => ar.Slug == slug);
            return article;
        }

        public void Delete(string slug, string authorEmail)
        {
            var article = _context.Articles.Include(a => a.User).First(art => (art.Slug == slug) && (art.User.Email == authorEmail));
            _context.Articles.Remove(article);
            _context.SaveChanges();
        }

        public void Update(Article articleToSave, string authorEmail)
        {
            _context.SaveChanges(); ;
        }
        public List<Article> ListArticles(int limit, int offset)
        {
            List<Article> articles;
            articles = _context.Articles.Include(a => a.User).OrderByDescending(c => c.UpdatedAt).Skip(offset).Take(limit).ToList();
            return articles;
        }
        public List<Article> ListArticlesByTag(string tag,int limit,int offset)
        {
            List<Article> articles;
            articles = _context.Articles.Include(a => a.User).Where(a => a.TagList.Contains(tag)).OrderByDescending(c => c.UpdatedAt).Skip(offset).Take(limit).ToList();
            return articles;
        }
        public List<Article> ListArticlesByAuthor(string authorName,int limit,int offset)
        {
            List<Article> articles;
            articles = _context.Articles.Include(a => a.User).Where(a => a.User.Username.Equals(authorName)).OrderByDescending(c => c.UpdatedAt).Skip(offset).Take(limit).ToList();
            return articles;
        }
        public List<Article> ListArticlesByFavorited(string favorited, int limit, int offset)
        {
            List<Article> articles;
            articles = _context.Users.Include(u => u.FavoritedArticles).First(u => u.Username == favorited).FavoritedArticles.OrderByDescending(c => c.UpdatedAt).Skip(offset).Take(limit).ToList();
            return articles;
        }

        public List<Article> FeedArticles(User currentUser, int limit, int offset)
        {
            List<Article> articles=new();
            var follwoings = _context.Users.Include(u => u.Followings).ThenInclude(u=>u.Articles).FirstOrDefault(u => u.Id == currentUser.Id).Followings.ToList();
            var r2 = follwoings.SelectMany(f=>f.Articles).Skip(offset).Take(limit).ToList();

            return r2;
        }

        public void FavoriteArticle(User currentUser, Article favoritedArticle)
        {
            favoritedArticle.FavoritesCount += 1;
            favoritedArticle.Favorited = true;
            _context.Users.First(u => u.Id == currentUser.Id).FavoritedArticles.Add(favoritedArticle);
            _context.SaveChanges();
        }
        public void UnFavoriteArticle(User currentUser, Article unFavoritedArticle)
        {
            unFavoritedArticle.FavoritesCount -= 1;
            if (unFavoritedArticle.FavoritesCount <= 0) unFavoritedArticle.Favorited = false;
            var result2 = _context.Users.Include(u => u.FavoritedArticles).FirstOrDefault(u => u.Id == currentUser.Id).FavoritedArticles.Remove(unFavoritedArticle);
            _context.SaveChanges();
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
            var comments = _context.Articles.Include(a => a.Comments).ThenInclude(a=>a.Author).First(a => a.Slug == slug).Comments;
            return comments;
        }

        public void DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }

        public List<string> GetTags()
        {
            var l = _context.Articles.Select(u=>u.TagList).ToList();
            List<string> result = new();
            foreach(var r in l)
            {
                if(r!=null)
                result.AddRange(r.Split(",").ToList());
            }

            return result.Distinct().ToList();
        }
    }
}