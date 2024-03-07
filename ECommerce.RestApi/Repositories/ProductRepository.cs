using ECommerce.RestApi.Models;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace ECommerce.RestApi.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductRepository(ECommerceContext mongoContext) : base(mongoContext)
        {
            _productCollection = mongoContext.Products;
        }

        public async Task<IEnumerable<TResult>> GetByCategoryAsync<TResult>(string categoryId, Expression<Func<Product, TResult>> selector)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.CategoryId, categoryId);
            var result = await _productCollection
                .Find(filter)
                .Project(selector).ToListAsync();
            return result;
        }
    }
}
