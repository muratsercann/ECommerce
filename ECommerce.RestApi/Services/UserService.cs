using ECommerce.RestApi.Models;
using ECommerce.RestApi.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;
using System.Formats.Asn1;

namespace ECommerce.RestApi.Services
{
    public class UserService : IUserService
    {
        private readonly ECommerceContext _mongoContext;
        private readonly IProductService _productService;

        public UserService(ECommerceContext mongoContext, IProductService productService)
        {
            _mongoContext = mongoContext;
            _productService = productService;
        }

        public async Task<List<User>> CreateManyAsync(List<User> users)
        {
            await _mongoContext.Users.InsertManyAsync(users);
            return users;
        }

        public async Task<User> CreateOneAsync(User user)
        {
            await _mongoContext.Users.InsertOneAsync(user);
            return user;
        }

        public Task<bool> DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _mongoContext.Users.AsQueryable().ToListAsync();
        }

        public async Task<User> GetUserAsync(string userId)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var user = await _mongoContext.Users.Find(filter).FirstOrDefaultAsync();
            return user;
        }

        public async Task<Cart> GetUserCart(string userId)
        {
            var user = await GetUserAsync(userId);

            if (user.Cart is null || user.Cart.Items == null || user.Cart.Items.Count == 0)
            {
                return new Cart();//???
            }

            var productIds = user.Cart.Items.Select(i => i.ProductId).ToList();
            var cartProducts = await _productService.GetProductsAsync(productIds);
            var totalPrice = cartProducts.Sum(p => p.Price * GetCartItemCount(user.Cart,p.Id));
            user.Cart.TotalPrice = totalPrice;

            return user.Cart;

            int GetCartItemCount(Cart cart,string productId)
            {
                var item = cart.Items.Where(i => i.ProductId == productId).FirstOrDefault();
                return item?.Quantity ?? 0;
            }

            
        }

        public async Task<long> GetUsersCountAsync()
        {
            return await _mongoContext.Users.CountDocumentsAsync(FilterDefinition<User>.Empty);
        }

        public async Task<bool> IsExistingUser(string username)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);
            var result = await _mongoContext.Users.Find(filter).FirstOrDefaultAsync();

            return result is null ? false : true;
        }

        public Task<User> UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> UpdateCartAsync(string userId, Cart cart)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var update = Builders<User>.Update.Set(u => u.Cart, cart);
            var result = await _mongoContext.Users.UpdateOneAsync(filter, update);

            return cart;
        }

        public async Task<List<string>> UpdateFavoritesAsync(string userId, List<string> favorites)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var update = Builders<User>.Update.Set(u => u.Favorites, favorites);
            var result = await _mongoContext.Users.UpdateOneAsync(filter, update);

            return favorites;
        }

        public async Task<IEnumerable<Product>> GetFavoriteProductsAsync(string userId)
        {
            var user = await this.GetUserAsync(userId);

            if (user is null || user.Favorites is null)
            {
                return new List<Product>();
            }

            var filter = Builders<Product>.Filter.In(p => p.Id, user.Favorites.Select(f => f.ToString()));
            var products = await _mongoContext.Products.Find(filter).ToListAsync();

            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsInTheCartAsync(string userId)
        {
            var user = await this.GetUserAsync(userId);

            if (user?.Cart?.Items is null || user.Cart.Items.Count == 0)
            {
                return new List<Product>();
            }

            var filter = Builders<Product>.Filter.In(p => p.Id, user.Cart.Items.Select(f => f.ProductId));
            var products = await _mongoContext.Products.Find(filter).ToListAsync();

            return products;
        }

    }
}
