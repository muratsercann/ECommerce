using ECommerce.RestApi.Dto;
using ECommerce.RestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _productService.GetProductsDtoAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _productService.GetProductDtoAsync(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductDto productDto)
        {
            var result = await _productService.CreateOneAsync(productDto);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string productId)
        {
            var result = await _productService.DeleteAsync(productId);
            return Ok(result);
        }
    }
}
