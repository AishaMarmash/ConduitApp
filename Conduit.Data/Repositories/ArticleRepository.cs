using AutoMapper;
using Conduit.Domain.Entities;
using Conduit.Domain.Repositories;
using Conduit.Domain.ViewModels.RequestBody;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            User user = _context.Users.SingleOrDefault(u => u.Email == authorEmail);
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
            //User user = _context.Users.SingleOrDefault(u => u.Email == authorEmail);
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
    }
}