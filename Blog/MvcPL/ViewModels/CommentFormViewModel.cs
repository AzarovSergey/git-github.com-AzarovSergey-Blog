using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.ViewModels
{
    public class CommentFormViewModel
    {
        [Required(ErrorMessage = "The field can not be empty!")]
        [DataType(DataType.MultilineText)]
        [MaxLength(2500)]
        public string Comment { get; set; }

        [Required]
        public int ArticleId { get; set; }

        [Required]
        public int CommentId { get; set; }
    }
}