using Microsoft.AspNetCore.Mvc;

namespace ECommerce.RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }
    }
}
