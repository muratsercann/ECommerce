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
        public async Task<IActionResult> Get()
        {
            var result = await _productService.GetProductsDtoAsync();
            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> Get(string productId)
        {
            var result = await _productService.GetProductDtoAsync(productId);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddProductDto addProductDto)
        {
            var result = await _productService.CreateOneAsync(addProductDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string productId)
        {
            var result = await _productService.DeleteAsync(productId);
            return Ok(result);
        }
    }
}
