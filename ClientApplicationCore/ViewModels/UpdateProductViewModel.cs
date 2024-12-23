using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ClientApplicationCore.ViewModels
{
    public class UpdateProductViewModel
    {

        [Required(ErrorMessage = "Product name is required")]
        [DisplayName("Product name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product description is required")]
        [DisplayName("Product description")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Product price is required")]
        [DisplayName("Product price")]
        //[DataType]
        public decimal ProductPrice { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [DisplayName("Category")]
        
        public int CategoryId { get; set; }


        public List<CategoryViewModel>? Categories { get; set; }

        [DisplayName("Is available")]
        public bool InStock { get; set; }

        [DisplayName("Is active")]
        public bool IsActive { get; set; }

        public int ProductId { get; set; }
    }
}
