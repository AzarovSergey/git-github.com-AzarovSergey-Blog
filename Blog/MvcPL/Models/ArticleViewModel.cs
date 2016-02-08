using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class ArticleViewModel
    {
        public ArticleViewModel() { }
        public ArticleViewModel(string Content,DateTime CreationDateTime,UserViewModel Author=null)
        {
            this.Content = Content;
            this.CreationDateTime = CreationDateTime;
            this.Author = Author;
        }

        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime CreationDateTime { get; set; }
        public UserViewModel Author { get; set; }
        public int AuthorId { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
        public int ArticleId { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}