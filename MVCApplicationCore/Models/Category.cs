﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCApplicationCore.Models
{
    public class Category
    {
        [Key]
        [DisplayName("Category id")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }

        public string FileName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}