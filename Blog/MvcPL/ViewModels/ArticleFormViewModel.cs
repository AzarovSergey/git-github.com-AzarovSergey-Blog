using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.ViewModels
{
    public class ArticleFormViewModel
    {
        [Required]
        [Display(Name ="Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The field can not be empty!")]
        [Display(Name = "Type your article here")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Display(Name = "Tags for article.")]
        public string Tags { get; set; }
        
    }
}