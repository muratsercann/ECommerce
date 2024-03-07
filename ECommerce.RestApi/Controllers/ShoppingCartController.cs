using ECommerce.RestApi.Models;
using ECommerce.RestApi.Dto;
using ECommerce.RestApi.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ECommerce.RestApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IUserService userService, IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string userId)
        {
            var cartDto = await _shoppingCartService.GetShoppingCartAsync(userId);
            return Ok(cartDto);
        }

        [HttpPut("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCartObject)
        {
            bool result = await _shoppingCartService.AddToCartAsync(addToCartObject);
            return Ok(result);
        }

        [HttpPut("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] AddToCartDto cartDto)
        {
            bool result = await _shoppingCartService.RemoveFromCartAsync(cartDto);
            return Ok(result);
        }

        [HttpPut("removeall")]
        public async Task<IActionResult> RemoveAllFromCart(string userId)
        {
            bool result = await _shoppingCartService.RemoveAllFromCartAsync(userId);
            return Ok(result);
        }

    }
}
