using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    [Table("Article")]
    public partial class Article
    {
        public Article()
        {
            Tags = new List<Tag>();
            CreationDateTime = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreationDateTime { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }


        public virtual User Author { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
