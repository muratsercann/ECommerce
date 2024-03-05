using AutoMapper;
using ECommerce.RestApi.Models;
using ECommerce.RestApi.Models.DTOs;
using MongoDB.Driver;

namespace ECommerce.RestApi.Services
{
    public class FavoritesService : IFavoritesService
    {
        private readonly ECommerceContext _mongoContext;
        private readonly IMapper _mapper;

        public FavoritesService(ECommerceContext mongoContext, IMapper mapper)
        {
            _mongoContext = mongoContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetFavoriteProductsDtoAsync(string userId)
        {
            var favorites = await GetFavoriteProductsAsync(userId);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(favorites);
            return productsDto;
        }

        public async Task<IEnumerable<Product>> GetFavoriteProductsAsync(string userId)
        {
            var favorites = await GetUserFavorites(userId);
            var filter = Builders<Product>.Filter.In(p => p.Id, favorites);
            var products = await _mongoContext.Products.Find(filter).ToListAsync();
            return products;
        }

        
        public async Task<List<string>> UpdateFavoritesAsync(string userId, List<string> favorites)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var update = Builders<User>.Update.Set(u => u.Favorites, favorites);
            var result = await _mongoContext.Users.UpdateOneAsync(filter, update);

            return favorites;
        }


        public async Task<bool> AddToFavorites(string userId, string productId)
        {
            var user = await GetUserAsync(userId);

            if (user is null)
            {
                return false;
            }

            var isExistingProduct = await IsExistingProductAsync(productId);

            if (!isExistingProduct)
            {
                return false;
            }

            if (user.Favorites is null)
            {
                user.Favorites = new List<string> { productId };
                await UpdateFavoritesAsync(userId, user.Favorites);
                return true;
            }
            else if (IsExistInFavorites(user, productId))
            {
                return false;
            }

            user.Favorites.Add(productId);
            await UpdateFavoritesAsync(userId, user.Favorites);
            return true;

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

            await UpdateFavoritesAsync(userId, user.Favorites);

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

            user.Favorites = new List<string>();

            await UpdateFavoritesAsync(userId, user.Favorites);

            return true;
        }

        private async Task<User> GetUserAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            var user = await _mongoContext.Users.Find(filter).FirstOrDefaultAsync();
            return user;
        }

        private async Task<IEnumerable<string>> GetUserFavorites(string userId)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(user => user.Id, userId);

            List<string> favoriteIds = await _mongoContext.Users
              .Find(u => u.Id == userId)
              .Project(u =>
                  u.Favorites
              ).FirstOrDefaultAsync();

            return favoriteIds;
        }

        private async Task<IEnumerable<Product>> GetProductsAsync(IEnumerable<string> productIds)
        {
            var filter = Builders<Product>.Filter.In(u => u.Id, productIds);
            var products = await _mongoContext.Products.Find(filter).ToListAsync();
            return products;
        }

        private async Task<bool> IsExistingProductAsync(string productId)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, productId);
            var isExist = await _mongoContext.Products.Find(filter).AnyAsync();

            return isExist;
        }

        private bool IsExistInFavorites(User user, string productId)
        {
            return user.Favorites.Any(item => item.ToString() == productId);
        }
    }
}
