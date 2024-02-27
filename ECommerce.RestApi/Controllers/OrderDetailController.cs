using Microsoft.AspNetCore.Mvc;

namespace ECommerce.RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderDetailController : Controller
    {
        private readonly ILogger<OrderDetailController> _logger;

        public OrderDetailController(ILogger<OrderDetailController> logger)
        {
            _logger = logger;
        }
    }
}
