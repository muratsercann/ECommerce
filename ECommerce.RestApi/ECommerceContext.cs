using ECommerce.RestApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ECommerce.RestApi
{
  
    public class ECommerceContext
    {  
        private readonly IMongoDatabase _database;

        public ECommerceContext(IOptions<ECommerceDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        private string GetCollectionName<T>()
        {
            return typeof(T).Name.ToLowerInvariant();
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>(GetCollectionName<User>());
        public IMongoCollection<Product> Products => _database.GetCollection<Product>(GetCollectionName<Product>());
        public IMongoCollection<Category> Categories => _database.GetCollection<Category>(GetCollectionName<Category>());
        public IMongoCollection<Order> Orders => _database.GetCollection<Order>(GetCollectionName<Order>());
        public IMongoCollection<ShoppingCart> Carts => _database.GetCollection<ShoppingCart>(GetCollectionName<ShoppingCart>());
        public IMongoCollection<ShoppingCartItem> CartItems => _database.GetCollection<ShoppingCartItem>(GetCollectionName<ShoppingCartItem>());
        
    }

    


}
