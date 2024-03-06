using ECommerce.RestApi.Models;
using ECommerce.RestApi.Dto;

namespace ECommerce.RestApi.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetProductDtoAsync(string productId);

        Task<IEnumerable<ProductDto>> GetProductsDtoAsync(IEnumerable<string> productIds);

        Task<IEnumerable<ProductDto>> GetProductsDtoAsync();

        Task<IEnumerable<ProductDto>> GetProductsDtoByCategoryAsync(string categoryId);

        Task<long> GetCountAsync();

        Task<bool> CreateOneAsync(CreateProductDto productDto);

        Task<bool> CreateManyAsync(IEnumerable<ProductDto> products);

        Task<bool> DeleteAsync(string productId);

        Task<bool> ExistsProductAsync(string productId);
    }
}
