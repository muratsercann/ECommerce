using ECommerce.RestApi.Dto;
using ECommerce.RestApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace ECommerce.RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : Controller
    {
        private readonly IFavoritesService _favoritesService;

        public FavoritesController(IFavoritesService favoritesService)
        {
            _favoritesService = favoritesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string userId)
        {
            IEnumerable<ProductDto> favorites = await _favoritesService.GetFavoriteProductsDtoAsync(userId);
            return Ok(favorites);
        }


        [HttpPut("add")]
        public async Task<IActionResult> AddToFavorites(string userId, string productId)
        {
            var result = await _favoritesService.AddToFavoritesAsync(userId, productId);
            return Ok(result);
        }


        [HttpPut("remove")]
        public async Task<IActionResult> RemoveFromFavorites(string userId, string productId)
        {
            var result = await _favoritesService.RemoveFromFavoritesAsync(userId, productId);
            return Ok(result);
        }


        [HttpPut("removeall")]
        public async Task<IActionResult> RemoveAllFavorites(string userId)
        {
            var result = await _favoritesService.RemoveAllFavoritesAsync(userId);
            return Ok(result);
        }

    }
}
