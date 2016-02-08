using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalArticle:IEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDateTime { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
    }
}
