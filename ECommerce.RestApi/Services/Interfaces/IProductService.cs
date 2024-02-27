using ECommerce.RestApi.Models;
using System.Data;

namespace ECommerce.RestApi.Services
{
    public interface IProductService
    {
        Task<Product> GetProductAsync(string ProductId);

        Task<long> GetCountAsync();

        Task<Product> CreateOneAsync(Product product);

        Task<List<Product>> CreateManyAsync(List<Product> products);

        Task<Product> UpdateAsync(Product product);

        Task<bool> DeleteAsync(string id);

    }
}
