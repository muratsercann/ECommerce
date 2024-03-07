using ECommerce.RestApi.Models;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace ECommerce.RestApi.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IMongoCollection<User> _userCollection;
        private readonly IMongoCollection<Product> _productCollection;
        public ShoppingCartRepository(ECommerceContext mongoContext)
        {
            _userCollection = mongoContext.Users;
            _productCollection = mongoContext.Products;
        }

        public async Task<ShoppingCart> GetShoppingCartAsync(string userId)
        {
            var filter = Builders<User>.Filter.Eq(user => user.Id, userId);
            ShoppingCart cart = await _userCollection.Find(filter)
                .Project(user => user.Cart).FirstOrDefaultAsync();

            return cart;
        }

        public async Task<IEnumerable<TResult>> GetProductsAsync<TResult>(IEnumerable<string> productIds, Expression<Func<Product, TResult>> selector)
        {
            var filter = Builders<Product>.Filter.In(product => product.Id, productIds);
            IEnumerable<TResult> products = await _productCollection.Find(filter).Project(selector).ToListAsync();
            return products;
        }

        public async Task<bool> UpdateShoppingCartAsync(string userId, ShoppingCart shoppingCart)
        {
            var filter = Builders<User>.Filter.Eq(user => user.Id, userId);
            var update = Builders<User>.Update.Set(user => user.Cart, shoppingCart);
            var result = await _userCollection.UpdateOneAsync(filter,update);
            return true;
        }
    }
}
