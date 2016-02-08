using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    [Table("Tag")]
    public partial class Tag
    {
        public Tag()
        {
            Articles = new List<Article>();//new HashSet<Article>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Value { get; set; }
        
        public virtual ICollection<Article> Articles { get; set; }
    }
}
