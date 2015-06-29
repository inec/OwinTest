using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo.MVC.FormAuthentication.Models
{
    public class LoginViewModel
    {
        [Required]
   //   [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public bool HasValidUsernameAndPassword
        {
            get
            {
                return Password == "password";
            }
        }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}