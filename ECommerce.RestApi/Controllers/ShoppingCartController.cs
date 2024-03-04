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
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDTO addToCartObject)
        {

            var user = await _userService.GetUserAsync(addToCartObject.userId);

            //msercan? : 
            //Her yerde user kontrolü yapıp geriye mesaj döndüreceksek bunu daha merkezi biryerden yapıp kod tekrarını önlememiz lazım.
            //Birde burası üst seviye bir metod. Başka birisi yarın başka bir yerden favorilere ekleme çıkarma ihtiyacı duysa
            //buradaki kontrollerin hepsini bilmesi lazım.

            if (user is null)
            {
                return BadRequest($"The user with the userId : {addToCartObject.userId} is not exist !");
            }

            //msercan? : Burada bu şekilde null kontrol yapmak mı. Yoksa User nesnesinde bunlara new ile ilk değer atamak mı.            
            //user.ShoppingCart ??= new ShoppingCart();
            //user.ShoppingCart.Items ??= new List<ShoppingCartItem>(); 

            var cartItem = user.Cart.Items.Where(item => item.ProductId == addToCartObject.productId).FirstOrDefault();

            if (cartItem is not null)
            {
                cartItem.Quantity += addToCartObject.quantity;
            }

            else
            {
                user.Cart.Items.Add(new ShoppingCartItem
                {
                    ProductId = addToCartObject.productId,
                    Quantity = addToCartObject.quantity,
                });
            }

            user.Cart.TotalItemCount = user.Cart.Items.Sum(item => item.Quantity);

            await _shoppingCartService.UpdateCartAsync(addToCartObject.userId, user.Cart);

            var updatedUser = await _userService.GetUserAsync(addToCartObject.userId);

            return Ok(updatedUser.Cart);
            //msercan? : Burda veritabanından güncel user bilgisini çekip geriye cart nesnesini gönderdik. Bu şekildeki işlem uygun mu ?
        }

        [HttpPut("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] AddToCartDTO cartDto)
        {
            var user = await _userService.GetUserAsync(cartDto.userId);

            if (user is null)
            {
                return BadRequest($"The user with the userId : {cartDto.userId} is not exist !");
            }

            if (user.Cart?.Items is null || user.Cart.Items.Count == 0)
            {
                return Ok($"The user with the userId : {cartDto.userId} does not have any product in his shopping cart to remove !");
            }

            var item = user.Cart.Items.Where(item => item.ProductId == cartDto.productId).FirstOrDefault();

            if (item is null)
            {
                return Ok($"This item already does not exist in the shopping cart of the user with userId : {cartDto.userId}.");
            }

            if (item.Quantity > cartDto.quantity)
            {
                item.Quantity -= cartDto.quantity;
            }
            else
            {
                user.Cart.Items.Remove(item);
            }

            user.Cart.TotalItemCount = user.Cart.Items.Sum(item => item.Quantity);

            await _shoppingCartService.UpdateCartAsync(cartDto.userId, user.Cart);

            var updatedUser = await _userService.GetUserAsync(cartDto.userId);

            return Ok(updatedUser.Cart);
        }



    }
}
