using ECommerce.RestApi.Models;
using ECommerce.RestApi.Models.DTOs;
using ECommerce.RestApi.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ECommerce.RestApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : Controller
    {
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly IUserService _userService;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(ILogger<ShoppingCartController> logger, IUserService userService, IShoppingCartService shoppingCartService)
        {
            _logger = logger;
            _userService = userService;
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string userId)
        {
            var user = await _userService.GetUserAsync(userId);

            if (user is null)
            {
                return NotFound($"The user with userId : {userId} is not exist");
            }

            if ((user.Cart?.Items?.Count ?? 0) == 0)//msercan?
            {
                return Ok("This user has not got an item in his shopping cart yet.");
            }

            IEnumerable<ProductDto> productsDtoInTheCart = await _shoppingCartService.GetProductsInTheCartAsync(userId);

            //msercan? :
            //UserService üzerinden direk ProductDto listesini döndürecek metod mu yazmalı.
            //Bu işlemleri nerede yapmalı. Burda olması ne kadar sağlıklı.

            List<ShoppingCartItemDto> cartItemDtoList = new List<ShoppingCartItemDto>();
            decimal totalPrice = 0;
            int totalItemCount = 0;
            foreach (var item in productsDtoInTheCart)
            {
                var quantity = user.Cart!.Items.Where(i => i.ProductId == item.Id).FirstOrDefault()?.Quantity ?? 0;
                totalItemCount += quantity;
                totalPrice += (quantity * item.Price);
                cartItemDtoList.Add(new ShoppingCartItemDto(item, quantity));
            }

            ShoppingCartDto cartDto = new ShoppingCartDto(cartItemDtoList, totalPrice, totalItemCount);

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
