using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Corezine.Site.Areas.Dashboard.ViewModels
{
    public class CreateCategoryModel
    {
        [Required]
        [Display(Name = "Category Name")]
        public String Name { get; set; }
    }
}
