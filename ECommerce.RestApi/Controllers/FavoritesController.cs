using ECommerce.RestApi.Dto;
using ECommerce.RestApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace ECommerce.RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : Controller
    {
        private readonly ILogger<FavoritesController> _logger;
        private readonly IFavoritesService _favoritesService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public FavoritesController(ILogger<FavoritesController> logger,IFavoritesService favoritesService ,  IUserService userService, IProductService productService)
        {
            _logger = logger;
            _favoritesService = favoritesService;
            _userService = userService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string userId)
        {
            var user = await _userService.GetAsync(userId);
            if (user == null)
            {
                return NotFound($"The user with userId {userId} does not exist");
            }

            if (user.Favorites is null || user.Favorites.Count == 0)
            {
                return Ok("The user with userId {userId}  does not have any favorite product!");
            }

            IEnumerable<ProductDto> favorites = await _favoritesService.GetFavoriteProductsDtoAsync(userId);
            
            return Ok(favorites);
        }


        [HttpPut("add")]
        public async Task<IActionResult> AddToFavorites(string userId, string productId)
        {
            var result = await _favoritesService.AddToFavorites(userId,productId);
            return Ok(result);

        }


        [HttpPut("remove")]
        public async Task<IActionResult> RemoveFromFavorites(string userId, string productId)
        {
            var result = await _favoritesService.RemoveFromFavorites(userId, productId);
            return Ok(result);
        }


        [HttpPut("removeall")]
        public async Task<IActionResult> RemoveAllFavorites(string userId)
        {
            var result = await _favoritesService.RemoveAllFavorites(userId);
            return Ok(result);
        }

    }
}
