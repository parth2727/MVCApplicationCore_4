﻿using MVCApplicationCore.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCApplicationCore.ViewModels
{
    public class ProductViewModel
    {
        [Required(ErrorMessage ="Product name is required")]
        [DisplayName("Product name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product description is required")]
        [DisplayName("Product description")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Product price is required")]
        [DisplayName("Product price")]
        public decimal ProductPrice { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        

        public List<Category>? Categories { get; set; }

        [DisplayName("Available")]
        public bool InStock { get; set; }

        [DisplayName("Is active")]
        public bool IsActive { get; set; }

        public int ProductId { get; set; }
    }
}