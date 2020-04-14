using Corezine.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Corezine.Domain.Models
{
    public class Post
    {
        [Key]
        public Int32 Id { get; set; }

        [MaxLength(200)]
        public String Title { get; set; }

        public String Content { get; set; }

        public PostStatus Status { get; set; } = PostStatus.Published;
        [MaxLength(255)]
        public String ThumbnailPath { get; set; } = "";

        public Int32 CategoryId { get; set; }

        public Int32 UserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedAt { get; set; }

        //Relationships
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        //Not Mapped Properties
        [NotMapped]
        public String ThumbnailUrl { get; set; }
    }
}
