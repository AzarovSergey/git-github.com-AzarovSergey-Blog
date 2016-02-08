using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreationDateTime { get; set; }
        public int AuthorId { get; set; }
        public int ArticleId { get; set; }
    }
}
