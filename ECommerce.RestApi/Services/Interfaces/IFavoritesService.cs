 using ECommerce.RestApi.Models;
using ECommerce.RestApi.Dto;

namespace ECommerce.RestApi.Services
{
    public interface IFavoritesService
    {
        Task<IEnumerable<ProductDto>> GetFavoriteProductsDtoAsync(string userId);
       
        Task<IEnumerable<ProductSummaryDto>> GetFavoriteProductsSummaryDtoAsync(string userId);

        Task<bool> AddToFavoritesAsync(string userId, string productId);

        Task<bool> RemoveFromFavoritesAsync(string userId, string productId);

        Task<bool> RemoveAllFavoritesAsync(string userId);
    }
}
