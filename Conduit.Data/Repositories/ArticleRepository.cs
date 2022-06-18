using Conduit.Domain.Entities;
using Conduit.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Data.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        protected readonly AppContext _context;

        public ArticleRepository(AppContext context)
        {
            _context = context;
        }
        public Article AddArticle(Article article, string authorEmail)
        {
            User user = _context.Users.First(u => u.Email == authorEmail);
            user.Articles.Add(article);
            _context.SaveChanges();
            var articleFromDB = FindArticle(article.Slug);
            return articleFromDB;
        }

        public Article? FindArticle(string slug)
        {
            var article = _context.Articles.Include(a=>a.User).FirstOrDefault(ar => ar.Slug == slug);
            return article;
        }

        public void DeleteArticle(string slug, string authorEmail)
        {
            var article = _context.Articles.Include(a => a.User).First(art => (art.Slug == slug) && (art.User.Email == authorEmail));
            _context.Articles.Remove(article);
            _context.SaveChanges();
        }

        public void UpdateArticle(Article articleToSave, string authorEmail)
        {
            _context.SaveChanges(); ;
        }
        public List<Article> ListArticles(int limit, int offset)
        {
            List<Article> articles;
            articles = _context.Articles.Include(a => a.User).OrderByDescending(c => c.CreatedAt).Skip(offset).Take(limit).ToList();
            return articles;
        }
        public List<Article> ListArticlesByTag(string tag,int limit,int offset)
        {
            List<Article> articles;
            articles = _context.Articles.Include(a => a.User).Where(a => a.TagList.Contains(tag)).OrderByDescending(c => c.CreatedAt).Skip(offset).Take(limit).ToList();
            return articles;
        }
        public List<Article> ListArticlesByAuthor(string authorName,int limit,int offset)
        {
            List<Article> articles;
            articles = _context.Articles.Include(a => a.User).Where(a => a.User.Username.Equals(authorName)).OrderByDescending(c => c.CreatedAt).Skip(offset).Take(limit).ToList();
            return articles;
        }
        public List<Article> ListArticlesByFavorited(string favorited, int limit, int offset)
        {
            List<Article> articles;
            articles = _context.Users.Include(u => u.FavoritedArticles).ThenInclude(u => u.User).First(u => u.Username == favorited).FavoritedArticles.OrderByDescending(c => c.CreatedAt).Skip(offset).Take(limit).ToList();
            return articles;
        }
        public List<Article> FeedArticles(User currentUser, int limit, int offset)
        {
            var follwoings = _context.Users.Include(u => u.Followings).ThenInclude(u=>u.Articles).First(u => u.Id == currentUser.Id).Followings.ToList();
            List<Article> articles = follwoings.SelectMany(f=>f.Articles).OrderByDescending(c=>c.CreatedAt).Skip(offset).Take(limit).ToList();
            return articles;
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
            _context.Users.Include(u => u.FavoritedArticles).First(u => u.Id == currentUser.Id).FavoritedArticles.Remove(unFavoritedArticle);
            _context.SaveChanges();
        }
        public List<string> GetTags()
        {
            var TagsList = _context.Articles.Select(u=>u.TagList).ToList();
            List<string> result = new();
            foreach(var Tags in TagsList)
            {
                if(!String.IsNullOrEmpty(Tags))
                result.AddRange(Tags.Split(",").ToList());
            }
            return result.Distinct().ToList();
        }
    }
}