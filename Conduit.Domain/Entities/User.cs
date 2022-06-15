using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Bio { get; set; }
        public string? Image { get; set; }
        public List<Article> Articles { get; set; } = new List<Article>();
        public List<Article> FavoritedArticles { get; set; } = new List<Article>();

        public List<User> Followings { get; set; } = new List<User>();
        public List<User> Followers { get; set; } = new List<User>();
        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}