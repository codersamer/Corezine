using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Corezine.Site.Areas.Frontend.ViewModels
{
    public class RegisterModel
    {
        [Required]
        public String Username { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public String ConfirmPassword { get; set; }

    }
}
