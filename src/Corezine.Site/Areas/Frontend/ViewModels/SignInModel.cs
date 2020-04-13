using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Corezine.Site.Areas.Frontend.ViewModels
{
    public class SignInModel
    {
        [Required]
        [Display(Name = "Username")]
        public String Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        public Boolean RememberMe { get; set; }
        public String ReturnUrl { get; set; }
    }
}
