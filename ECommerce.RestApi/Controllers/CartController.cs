using ECommerce.RestApi.DTOs;
using ECommerce.RestApi.Models;
using ECommerce.RestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.RestApi.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICartItemService _cartItemService;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartService cartService, ICartItemService cartItemService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _cartItemService = cartItemService;
            _logger = logger;
        }

        [HttpGet("getcart")]
        public async Task<IActionResult> GetCart(string userId)
        {
            var cart = await _cartService.GetCart(userId);

            if (cart != null)
            {
                //product, quantity, price
                List<CartItemDTO> products = await _cartService.GetProductsInfo(cart.Id);

                CartDTO cartDTO = new CartDTO(products, products.Sum(p => p.price), products.Sum(p=> p.quantity));

                return Ok(cartDTO);
            }

            else { return Ok(); }

        }

        [HttpPost("addtocart")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDTO addToCartObject)
        {
            var cart = await _cartService.GetCart(addToCartObject.userId);

            if (cart == null)
            {
                cart = await _cartService.CreateEmptyCart(addToCartObject.userId);
            }
            //todo : CreateAsync metod ismi AddToCartAsync olarak değiştirilebilir
            var cartItem = await _cartItemService.CreateAsync(cart.Id, addToCartObject.productId, addToCartObject.quantity);

            return Ok(cartItem);
        }
    }
}
