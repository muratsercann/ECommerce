 using ECommerce.RestApi.Models;
using ECommerce.RestApi.Dto;

namespace ECommerce.RestApi.Services
{
    public interface IFavoritesService
    {
        Task<IEnumerable<ProductDto>> GetFavoriteProductsDtoAsync(string userId);
       
        Task<IEnumerable<ProductSummaryDto>> GetFavoriteProductsSummaryDtoAsync(string userId);

        Task<bool> AddToFavorites(string userId, string productId);

        Task<bool> RemoveFromFavorites(string userId, string productId);

        Task<bool> RemoveAllFavorites(string userId);
    }
}
