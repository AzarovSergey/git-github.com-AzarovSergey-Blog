using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreationDateTime { get; set; }
        public int AuthorId { get; set; }
        public int ArticleId { get; set; }

        public string AuthorName { get; set; }
    }
}