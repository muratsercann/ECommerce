using ECommerce.RestApi.Models;
using ECommerce.RestApi.Models.DTOs;
using ECommerce.RestApi.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;


namespace ECommerce.RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : Controller
    {
        private readonly ILogger<FavoritesController> _logger;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public FavoritesController(ILogger<FavoritesController> logger, IUserService userService, IProductService productService)
        {
            _logger = logger;
            _userService = userService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string userId)
        {
            var user = await _userService.GetUserAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            if (user.Favorites is null || user.Favorites.Count == 0)
            {
                return Ok("The user does not have any favorite product!");
            }

            IEnumerable<Product> favorites = await _userService.GetFavoriteProductsAsync(userId);
            IEnumerable<ProductDTO> productsDto = Product.ConvertToProductDTO(favorites);

            return Ok(productsDto);
        }


        [HttpPut("add")]
        public async Task<IActionResult> AddToFavorites(string userId, string productId)
        {
            var user = await _userService.GetUserAsync(userId);

            if (user is null)
            {
                return BadRequest($"The user with the userId : {userId} is not exist !");
            }

            var isExistingProduct = await _productService.IsValidProductIdAsync(productId);

            if (!isExistingProduct)
            {
                return NotFound($"Invalid product id");
            }

            if (user.Favorites is null)
            {
                user.Favorites = new List<string> { productId };
                await _userService.UpdateFavoritesAsync(userId, user.Favorites);
                return Ok(user.Favorites);
            }
            else if (IsExistInFavorites(user, productId))
            {
                return Ok($"The user with userId : {userId} already has this product with id : {productId} already in his favorites");
            }

            user.Favorites.Add(productId);
            await _userService.UpdateFavoritesAsync(userId, user.Favorites);

            var favorites = await _userService.GetFavoriteProductsAsync(userId);

            return Ok(favorites);
        }


        [HttpPut("remove")]
        public async Task<IActionResult> RemoveFromFavorites(string userId, string productId)
        {
            var user = await _userService.GetUserAsync(userId);

            if (user is null)
            {
                return BadRequest($"The user with the userId : {userId} is not exist !");
            }

            if (user.Favorites is null || user.Favorites.Count == 0)
            {
                return Ok($"The user with the userId : {userId} does not have any product in his shopping cart to remove!");
            }

            var item = user.Favorites.Where(item => item.ToString() == productId).First();

            if (string.IsNullOrEmpty(item.ToString()))
            {
                return Ok($"This item already does not exist in the shopping cart of the user with userId : {userId}.");
            }

            user.Favorites.Remove(item);

            await _userService.UpdateFavoritesAsync(userId, user.Favorites);

            var favorites = await _userService.GetFavoriteProductsAsync(userId);

            return Ok(favorites);
        }


        private bool IsExistInFavorites(User user, string productId)
        {
            return user.Favorites.Any(item => item.ToString() == productId);
        }


    }
}
