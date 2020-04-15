using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
