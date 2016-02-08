using System;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.ViewModels
{
    public class RegisterViewModel
    {
        //[ScaffoldColumn(false)]
        //public int Id { get; set; }

        [Display(Name = "Enter your login")]
        [Required(ErrorMessage = "The field can not be empty!")]
        [StringLength(100)]
        public string Login { get; set; }

        [Display(Name = "Enter your name")]
        [Required(ErrorMessage = "The field can not be empty!")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm the password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm the password")]
        [Compare("Password", ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; }

    }
}