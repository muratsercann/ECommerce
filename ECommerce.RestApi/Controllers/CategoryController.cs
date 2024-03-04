using ECommerce.RestApi.Models.DTOs;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var result = await _categoryService.GetCategoriesAsync();
            return Ok(result);
        }


        /// <summary>
        /// Kategoriye ait ürün listesini döndürür.
        /// </summary>
        /// <param Name="categoryId">kategori id</param>
        /// <returns></returns>
        [HttpGet("Products/")]
        public async Task<IActionResult> Get(string categoryId)
        {
            var result = await _productService.GetProductsDtoByCategoryAsync(categoryId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post([FromBody] CategoryDto category)
        {
            var result = await _categoryService.CreateOneAsync(new Category { Name = category.Name, ParentId = category.ParentId, Description = category.Description });

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
