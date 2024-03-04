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
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public FavoritesService(ECommerceContext mongoContext,IMapper mapper, IUserService userService, IProductService productService)
        {
            _mongoContext = mongoContext;
            _mapper = mapper;
            _userService = userService;
            _productService = productService;
        }
        
        public async Task<IEnumerable<Product>> GetFavoriteProductsAsync(string userId)
        {
            var user = await _userService.GetUserAsync(userId);

            if (user is null || user.Favorites is null)
            {
                return new List<Product>();
            }

            var filter = Builders<Product>.Filter.In(p => p.Id, user.Favorites.Select(f => f.ToString()));
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

        public async Task<IEnumerable<ProductDto>> GetFavoriteProductsDtoAsync(string userId)
        {
            var user = await _userService.GetUserAsync(userId);
            var products = await  _productService.GetProductsDtoAsync(user.Favorites);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;            
        }

        public async Task<bool> AddToFavorites(string userId, string productId)
        {
            var user = await _userService.GetUserAsync(userId);

            if (user is null)
            {
                return false;
            }

            var isExistingProduct = await _productService.IsValidProductIdAsync(productId);

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

        private bool IsExistInFavorites(User user, string productId)
        {
            return user.Favorites.Any(item => item.ToString() == productId);
        }

        public async Task<bool> RemoveFromFavorites(string userId, string productId)
        {
            var user = await _userService.GetUserAsync(userId);

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
            var user = await _userService.GetUserAsync(userId);

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
    }
}
