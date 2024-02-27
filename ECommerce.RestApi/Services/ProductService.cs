using ECommerce.RestApi.Models;
using MongoDB.Driver;

namespace ECommerce.RestApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ECommerceContext _mongoContext;

        public ProductService(ECommerceContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task<List<Product>> CreateManyAsync(List<Product> products)
        {
            await _mongoContext.Products.InsertManyAsync(products);
            return products;
        }

        public async Task<Product> CreateOneAsync(Product product)
        {
            await _mongoContext.Products.InsertOneAsync(product);
            return product;
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<long> GetCountAsync()
        {
            return await _mongoContext.Products.CountDocumentsAsync(FilterDefinition<Product>.Empty);
        }

        public Task<Product> GetProductAsync(string ProductId)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
