using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    [Table("Comment")]
    public partial class Comment
    {
        public Comment()
        {
            CreationDateTime = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime CreationDateTime { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
        public virtual User Author { get; set; }
    }
}
