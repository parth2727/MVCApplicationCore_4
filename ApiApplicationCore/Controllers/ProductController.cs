using ApiApplicationCore.Dtos;
using ApiApplicationCore.Models;
using ApiApplicationCore.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplicationCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            var response = _productService.GetAllProducts();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct(AddProductDto addProduct)
        {
            var product = new Product
            {
                CategoryId = addProduct.CategoryId,
                ProductName = addProduct.ProductName,
                ProductDescription = addProduct.ProductDescription,
                ProductPrice = addProduct.ProductPrice,
                InStock = addProduct.InStock,
                IsActive = addProduct.IsActive,
            };
            var result = _productService.AddProduct(product);
            return !result.Success ? BadRequest(result) : Ok(result);
        }

        [HttpGet("GetProductById/{id}")]
        public IActionResult GetProductById(int id)
        {
            var response = _productService.GetProductById(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut("ModifyProduct")]
        public IActionResult UpdateProduct(UpdateProductDto productDto)
        {
            var product = new Product()
            {
                ProductId = productDto.ProductId,
                ProductName = productDto.ProductName,
                ProductDescription = productDto.ProductDescription,
                ProductPrice = productDto.ProductPrice,
                CategoryId = productDto.CategoryId,
                InStock = productDto.InStock,
                IsActive = productDto.IsActive,
            };

            var response = _productService.UpdateProduct(product);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult RemoveProduct(int id)
        {
            if (id > 0)
            {
                var response = _productService.DeleteProduct(id);
                if (!response.Success)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            else
            {
                return BadRequest("Please enter proper data.");
            }
        }
    }
}
