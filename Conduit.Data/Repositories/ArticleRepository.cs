using Conduit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Data.Repositories
{
    public class ArticleRepository
    {
        protected readonly AppContext _context;

        public ArticleRepository(AppContext context)
        {
            _context = context;
        }
        public void Add(Article article)
        {
            _context.Articles.Add(article);
            _context.SaveChanges();
        }

        public Article Find(string slug)
        {
            var article = _context.Articles.First(ar => ar.Slug == slug);
            return article;
        }

        public void Delete(Article article)
        {   
            _context.Articles.Remove(article);
            _context.SaveChanges();
        }
    }
}