using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Models
{
    public class ArticleForCreate
    {
        public string Title { get; set; }
        public string Description { get; set; } 
        public string Body { get; set; }
        public List<string> Tags { get; set; }
    }
}