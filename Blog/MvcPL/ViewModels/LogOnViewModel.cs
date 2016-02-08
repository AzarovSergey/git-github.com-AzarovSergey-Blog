using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MvcPL.ViewModels
{
    public class LogOnViewModel
    {
        [Required(ErrorMessage = "The field can not be empty!")]
        [Display(Name = "Enter your login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The field can not be empty!")]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        public string Password { get; set; }

        [Display(Name = "Remember user?")]
        public bool RememberMe { get; set; }

    }
}