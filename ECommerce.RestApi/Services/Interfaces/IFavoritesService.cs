 using ECommerce.RestApi.Models;
using ECommerce.RestApi.Models.DTOs;

namespace ECommerce.RestApi.Services
{
    public interface IFavoritesService
    {
        Task<IEnumerable<ProductDto>> GetFavoriteProductsDtoAsync(string userId);
       
        Task<IEnumerable<Product>> GetFavoriteProductsAsync(string userId);

        Task<bool> AddToFavorites(string userId, string productId);

        Task<bool> RemoveFromFavorites(string userId, string productId);

        Task<bool> RemoveAllFavorites(string userId);

        Task<List<string>> UpdateFavoritesAsync(string userId, List<string> favorites);
    }
}
