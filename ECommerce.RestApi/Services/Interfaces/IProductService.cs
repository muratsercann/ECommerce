using ECommerce.RestApi.Models;
using System.Data;

namespace ECommerce.RestApi.Services
{
    public interface IProductService
    {
        Task<Product> GetProductAsync(string productId);
        Task<List<Product>> GetProductsAsync(List<string> productIds);

        Task<List<Product>> GetProductsAsync();

        Task<List<Product>> GetProductsAsync(string categoryId);
                
        Task<long> GetCountAsync();

        Task<Product> CreateOneAsync(Product product);

        Task<List<Product>> CreateManyAsync(List<Product> products);

        Task<Product> UpdateAsync(Product product);

        Task<bool> DeleteAsync(string productId);

        Task<bool> IsValidProductIdAsync(string productId);

    }
}
