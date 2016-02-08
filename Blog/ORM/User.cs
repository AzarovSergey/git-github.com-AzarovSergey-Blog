using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Collections;
using System.Collections.Generic;

namespace ORM
{
    [Table("User")]
    public partial class User
    {
        public const int USERNAME_MAX_LENGTH = 100;
        public const int LOGIN_MAX_LENGTH = 100;
        public const int PASSWORD_MAX_LENGTH = 100;

        public User()
        {
            Articles = new List<Article>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(USERNAME_MAX_LENGTH)]
        public string UserName { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        [StringLength(LOGIN_MAX_LENGTH)]
        public string Login { get; set; }

        [Required]
        [StringLength(PASSWORD_MAX_LENGTH)]
        public string Password { get; set; }

        [Required]
        public bool Ban { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
