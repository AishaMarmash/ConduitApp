﻿namespace Conduit.Domain.ViewModels
{
    public class CreateArticleDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public List<string>? TagList { get; set; } = new();
    }
}