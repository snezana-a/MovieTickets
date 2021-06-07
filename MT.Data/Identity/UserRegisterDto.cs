using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MT.Data.Identity
{
    public class UserRegisterDto
    {
        [EmailAddress(ErrorMessage = "Invalid e-mail address")]
        [Required(ErrorMessage = "E-mail is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
