using Microsoft.AspNetCore.Mvc;

namespace ECommerce.RestApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }
    }
}
