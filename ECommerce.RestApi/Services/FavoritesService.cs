using AutoMapper;
using ECommerce.RestApi.Models;
using ECommerce.RestApi.Dto;
using MongoDB.Driver;
using ECommerce.RestApi.Repositories;

namespace ECommerce.RestApi.Services
{
    public class FavoritesService : IFavoritesService
    {
        private readonly ECommerceContext _mongoContext;//Should not be here. Remove it...
        private readonly IMapper _mapper;
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoritesService(ECommerceContext mongoContext, IMapper mapper, IFavoriteRepository favoriteRepository)
        {
            _mongoContext = mongoContext;
            _mapper = mapper;
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

        public async Task<bool> AddToFavorites(string userId, string productId)
        {
            //Edit the GetUserAsync method to use the Project to get only the required fields
            var user = await GetUserAsync(userId);
            bool result = false;
            if (user is null)
            {
                return result;
            }

            var isExistingProduct = await ExistingProductAsync(productId);

            if (!isExistingProduct)
            {
                return result;
            }

            if (user.Favorites is null)
            {
                user.Favorites = new List<string> { productId };
                result = await _favoriteRepository.UpdateFavoritesAsync(userId, user.Favorites);
                return result;
            }

            else if (ExistsInFavorites(user, productId))
            {
                return result;
            }

            user.Favorites.Add(productId);
            result = await _favoriteRepository.UpdateFavoritesAsync(userId, user.Favorites);
            return result;
        }

        public async Task<bool> RemoveFromFavorites(string userId, string productId)
        {
            var user = await GetUserAsync(userId);

            if (user is null)
            {
                return false;
            }

            if (user.Favorites is null || user.Favorites.Count == 0)
            {
                return false;
            }

            var item = user.Favorites.Where(item => item.ToString() == productId).FirstOrDefault();

            if (string.IsNullOrEmpty(item?.ToString()))
            {
                return false;
            }

            user.Favorites.Remove(item);

            await _favoriteRepository.UpdateFavoritesAsync(userId, user.Favorites);

            return true;
        }

        public async Task<bool> RemoveAllFavorites(string userId)
        {
            var user = await GetUserAsync(userId);

            if (user is null)
            {
                return false;
            }

            if (user.Favorites is null || user.Favorites.Count == 0)
            {
                return false;
            }

            await _favoriteRepository.UpdateFavoritesAsync(userId, new List<string>());

            return true;
        }

        //msercan Edit below

        private async Task<User> GetUserAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            var user = await _mongoContext.Users.Find(filter).FirstOrDefaultAsync();
            return user;
        }

        private async Task<bool> ExistingProductAsync(string productId)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, productId);
            var isExist = await _mongoContext.Products.Find(filter).AnyAsync();

            return isExist;
        }

        private bool ExistsInFavorites(User user, string productId)
        {
            return user.Favorites.Any(item => item.ToString() == productId);
        }
    }
}
