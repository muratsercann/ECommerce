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

        public async Task<IEnumerable<string>> GetFavoriteIdsAsync(string userId)
        {
            var filter = Builders<User>.Filter.Eq(user => user.Id, userId);
            var favoriteIds = await _userCollection.Find(filter)
                   .Project(user => user.Favorites).FirstOrDefaultAsync();

            return favoriteIds;
        }

        public async Task<IEnumerable<TResult>> GetByUserIdAsync<TResult>(string userId, Expression<Func<Product, TResult>> selector)
        {
            IEnumerable<string> favoriteIds = await GetFavoriteIdsAsync(userId);
            IEnumerable<TResult> products = await GetProductsAsync(favoriteIds, selector);
            return products;
        }

        public async Task<bool> ExistingProductAsync(string productId)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, productId);
            bool isExist = await _productCollection.Find(filter).AnyAsync();
            return isExist;
        }


        public async Task<bool> UpdateFavoritesAsync(string userId,IEnumerable<string> favoriteIds)
        {
            var filter = Builders<User>.Filter.Eq(user => user.Id, userId);
            var update = Builders<User>.Update.Set(user => user.Favorites, favoriteIds);
            var result = await _userCollection.UpdateOneAsync(filter, update);
            return true;
        }

       

        private async Task<IEnumerable<TResult>> GetProductsAsync<TResult>(IEnumerable<string> favoriteIds, Expression<Func<Product,TResult>> selector)
        {
            var productFilter = Builders<Product>.Filter.In(product => product.Id, favoriteIds);
            List<TResult> products = await _productCollection.Find(productFilter)
                .Project(selector)
                .ToListAsync();

            return products;
        }

        
    }
}
