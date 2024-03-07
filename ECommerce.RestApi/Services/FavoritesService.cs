using AutoMapper;
using ECommerce.RestApi.Models;
using ECommerce.RestApi.Dto;
using MongoDB.Driver;
using ECommerce.RestApi.Repositories;

namespace ECommerce.RestApi.Services
{
    public class FavoritesService : IFavoritesService
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoritesService(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetFavoriteProductsDtoAsync(string userId)
        {
            IEnumerable<ProductDto> result = await _favoriteRepository.GetByUserIdAsync(userId, ProductDto.Selector);
            return result;
        }

        public async Task<IEnumerable<ProductSummaryDto>> GetFavoriteProductsSummaryDtoAsync(string userId)
        {
            IEnumerable<ProductSummaryDto> result = await _favoriteRepository.GetByUserIdAsync(userId, ProductSummaryDto.Selector);
            return result;
        }

        public async Task<bool> AddToFavoritesAsync(string userId, string productId)
        {
            List<string> userFavorites = (await _favoriteRepository.GetFavoriteIdsAsync(userId)).ToList();
            
            bool result = false;
            
            if (userFavorites is null)
            {
                return false;
            }

            var isExistingProduct = await _favoriteRepository.ExistingProductAsync(productId);

            if (!isExistingProduct)
            {
                return false;
            }

            if (userFavorites is null)
            {
                userFavorites = new List<string> { productId };
                result = await _favoriteRepository.UpdateFavoritesAsync(userId, userFavorites);
                return result;
            }

            else if (ExistsInFavorites(userFavorites, productId))
            {
                return false;
            }

            userFavorites.Add(productId);
            result = await _favoriteRepository.UpdateFavoritesAsync(userId, userFavorites);
            return result;
        }

        public async Task<bool> RemoveFromFavoritesAsync(string userId, string productId)
        {
            List<string> userFavorites = (await _favoriteRepository.GetFavoriteIdsAsync(userId)).ToList();


            if (userFavorites is null)
            {
                return false;
            }

            if (userFavorites is null || userFavorites.Count == 0)
            {
                return false;
            }

            var item = userFavorites.Where(item => item.ToString() == productId).FirstOrDefault();

            if (string.IsNullOrEmpty(item?.ToString()))
            {
                return false;
            }

            userFavorites.Remove(item);
            bool result = await _favoriteRepository.UpdateFavoritesAsync(userId, userFavorites);
            return result;
        }

        public async Task<bool> RemoveAllFavoritesAsync(string userId)
        {
            bool result = await _favoriteRepository.UpdateFavoritesAsync(userId, new List<string>());
            return result;
        }

        private bool ExistsInFavorites(IEnumerable<string> favoriteIds, string productId)
        {
            bool isExist = favoriteIds.Any(item => item.ToString() == productId);
            return isExist;
        }
    }
}
