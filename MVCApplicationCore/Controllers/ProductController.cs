using Microsoft.AspNetCore.Mvc;
using MVCApplicationCore.Models;
using MVCApplicationCore.Services.Contract;
using MVCApplicationCore.ViewModels;

namespace MVCApplicationCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService,ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Categories = new List<Models.Category>();
            productViewModel.Categories = _categoryService.GetCategories().ToList();
            
            return View(productViewModel);
        }
        [HttpPost]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            if(ModelState.IsValid)
            {
                var product = new Product()
                {
                    CategoryId = productViewModel.CategoryId,
                    ProductName = productViewModel.ProductName,
                    ProductDescription = productViewModel.ProductDescription,
                    ProductPrice = productViewModel.ProductPrice,
                    InStock = productViewModel.InStock,
                    IsActive = productViewModel.IsActive,
                };
                var message = _productService.AddProduct(product);
                if(!string.IsNullOrWhiteSpace(message))
                {
                    if(message == "Something went wrong, Please try after sometime." || message == "Product already exists.")
                    {
                        TempData["ErrorMessage"] = message;
                    }
                    else
                    {
                        TempData["SuccessMessage"] = message;
                        return RedirectToAction("Index");
                    }
                }
            }
            productViewModel.Categories = new List<Models.Category>();
            productViewModel.Categories = _categoryService.GetCategories().ToList();
            return View(productViewModel);
        }
        public IActionResult Details(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            ProductViewModel viewModel = new ProductViewModel()
            {
                ProductId = product.ProductId,
                CategoryId = product.CategoryId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                InStock = product.InStock,
                IsActive = product.IsActive,
                Categories = _categoryService.GetCategories().ToList(),
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            Product product = new Product();
            if (ModelState.IsValid)
            {
                product = new Product()
                {
                    ProductId = productViewModel.ProductId,
                    CategoryId = productViewModel.CategoryId,
                    ProductName = productViewModel.ProductName,
                    ProductDescription = productViewModel.ProductDescription,
                    ProductPrice = productViewModel.ProductPrice,
                    InStock = productViewModel.InStock,
                    IsActive = productViewModel.IsActive,
                };
                var message = _productService.UpdateProduct(product);
                if (message == "Product already exists." || message == "Something went wrong, please try after sometime.")
                {
                    TempData["ErrorMessage"] = message;
                }
                else
                {
                    TempData["SuccessMessage"] = message;
                    return RedirectToAction("Index","Product");
                }
            }
            return View(product);
        }
        public IActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int productId)
        {
            var result = _productService.DeleteProduct(productId);
            if (result == "Product deleted successfully.")
            {
                TempData["SuccessMessage"] = result;
            }
            else
            {
                TempData["ErrorMessage"] = result;
            }

            return RedirectToAction("Index");
        }
    }
}
