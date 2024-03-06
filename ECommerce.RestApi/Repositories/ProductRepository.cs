using ECommerce.RestApi.Models;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace ECommerce.RestApi.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ECommerceContext _mongoContext;
        private readonly IMongoCollection<Product> _collection;

        public ProductRepository(ECommerceContext mongoContext) : base(mongoContext)
        {
            _mongoContext = mongoContext;
            _collection = _mongoContext.GetCollection<Product>();
        }

        public async Task<IEnumerable<TResult>> GetByCategoryAsync<TResult>(string categoryId, Expression<Func<Product, TResult>> selector)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.CategoryId, categoryId);
            var result = await _collection
                .Find(filter)
                .Project(selector).ToListAsync();
            return result;
        }
    }
}
