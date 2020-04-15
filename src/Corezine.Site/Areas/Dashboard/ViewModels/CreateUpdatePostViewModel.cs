using Corezine.Domain.Contracts;
using Corezine.Domain.Enums;
using Corezine.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Corezine.Site.Areas.Dashboard.ViewModels
{
    public class CreateUpdatePostViewModel
    {
        public Int32 Id { get; set; }
        [Required]
        [MinLength(5)]
        public String Title { get; set; }
        public String Content { get; set; }
        public PostStatus Status { get; set; }
        
        public IFormFile Thumbnail { get; set; }
        [FileExtensions(Extensions = "jpg,png,jpeg,", ErrorMessage = "Please Provide a valid Image File")]
        public String ThumbnailName { get { return Thumbnail == null ? "" : Thumbnail.FileName; }}
        [Display(Name = "Category")]
        public Int32 CategoryId { get; set; }

        public Boolean EditMode { get { return Id > 0; } }

        public IEnumerable<Category> Categories { get; set; }

    }
}
