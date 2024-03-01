using ECommerce.RestApi.Models;
using ECommerce.RestApi.Models.DTOs;
using ECommerce.RestApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ECommerce.RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var result = await _productService.GetProductsAsync();
            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<Product>> Get(string productId)
        {
            var result = await _productService.GetProductAsync(productId);
            return Ok(result);

        }


        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] ProductDTO product)
        {
            var result = await _productService.CreateOneAsync(
                new Product
                {
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    Name = product.Name,
                    Price = product.Price,
                    Rating = product.Rating,
                    Stock = product.Stock
                });

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(string productId)
        {
            var result = await _productService.DeleteAsync(productId);
            return Ok(result);
        }
    }
}
