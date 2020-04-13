using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Corezine.Domain.Models
{
    public class Category
    {
        [Key]
        public Int32 Id { get; set; }

        [MaxLength(100)]
        public String Name { get; set; } = "Uncategorized";
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
