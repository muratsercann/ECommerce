using ECommerce.RestApi.Models;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace ECommerce.RestApi.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly IMongoCollection<User> _userCollection;
        private readonly IMongoCollection<Product> _productCollection;
        public FavoriteRepository(ECommerceContext mongoContext)
        {
            _userCollection = mongoContext.Users;
            _productCollection = mongoContext.Products;

        }


        public async Task<IEnumerable<TResult>> GetByUserIdAsync<TResult>(string userId, Expression<Func<Product, TResult>> selector)
        {
            var filter = Builders<User>.Filter.Eq(user => user.Id, userId);
            var favoriteIds = await _userCollection.Find(filter)
                    .Project(user => user.Favorites).FirstOrDefaultAsync();

            var productFilter = Builders<Product>.Filter.In(product => product.Id, favoriteIds);
            List<TResult> products = await _productCollection.Find(productFilter)
                .Project(selector)
                .ToListAsync();

            return products;
        }


        public async Task<bool> UpdateFavoritesAsync(string userId,IEnumerable<string> favoriteIds)
        {
            var filter = Builders<User>.Filter.Eq(user => user.Id, userId);
            var update = Builders<User>.Update.Set(user => user.Favorites, favoriteIds);
            var result = await _userCollection.UpdateOneAsync(filter, update);
            return true;
        }
    }
}
