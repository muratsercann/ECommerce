using ECommerce.RestApi.Models;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;

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

        public async Task<bool> DeleteAsync(string productId)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, productId);
            var result = await _mongoContext.Products.DeleteOneAsync(filter);
            return result.IsAcknowledged;
        }

        public async Task<long> GetCountAsync()
        {
            return await _mongoContext.Products.CountDocumentsAsync(FilterDefinition<Product>.Empty);
        }

        public async Task<Product> GetProductAsync(string productId)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id ,productId) ;
            return await _mongoContext.Products.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetProductsAsync(List<string> productIds)
        {

            var filter = Builders<Product>.Filter.In(u => u.Id, productIds);
            var cartProducts = await _mongoContext.Products.Find(filter).ToListAsync();
            return cartProducts;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _mongoContext.Products.Find(Builders<Product>.Filter.Empty).ToListAsync();
        }

        public async Task<List<Product>> GetProductsAsync(string categoryId)
        {
            return await _mongoContext.Products.Find(Builders<Product>.Filter.Eq(p => p.CategoryId,categoryId)).ToListAsync();
        }

        public async Task<bool> IsValidProductIdAsync(string productId)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, productId);
            var isExist = await _mongoContext.Products.Find(filter).AnyAsync();

            return isExist;
        }

        /// <summary>
        ///!Not implemented yet.
        /// </summary>
        /// <param Name="product"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Product> UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
