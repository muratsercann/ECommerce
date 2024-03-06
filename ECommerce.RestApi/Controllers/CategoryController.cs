using ECommerce.RestApi.Dto;
using ECommerce.RestApi.Models;
using ECommerce.RestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IProductService _productService;
        private readonly IProductService _product;
        private readonly ICategoryService _categoryService;

        public CategoryController(ILogger<CategoryController> logger, IProductService productService, ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _categoryService.GetCategoryDtoAsync(id);
            return Ok(result);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var result = await _categoryService.GetCategoriesDtoAsync();
            return Ok(result);
        }


        [HttpGet("{categoryId}/products/")]
        public async Task<IActionResult> GetProducts(string categoryId)
        {
            var result = await _productService.GetProductsDtoByCategoryAsync(categoryId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post([FromBody] CreateCategoryDto category)
        {
            var result = await _categoryService.CreateOneAsync(category);
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
            var result = await _categoryService.DeleteAsync(productId);
            return Ok(result);
        }
    }
}
