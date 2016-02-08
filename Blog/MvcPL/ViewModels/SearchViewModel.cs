using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.ViewModels
{
    public class SearchViewModel
    {
        [Required(ErrorMessage = "The field can not be empty!")]
        [MinLength(1, ErrorMessage = "enter a search request")]
        [MaxLength(100, ErrorMessage = "Your request is too large.")]
        [Display(Name = "Search")]
        public string SearchString { get; set; }


        [Display(Name = "Search into tags")]
        public bool IsSearchByTag { get; set; }
    }
}