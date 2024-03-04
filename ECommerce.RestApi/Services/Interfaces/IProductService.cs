using ECommerce.RestApi.Models;
using ECommerce.RestApi.Models.DTOs;
using System.Data;

namespace ECommerce.RestApi.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetProductDtoAsync(string productId);

        Task<IEnumerable<ProductDto>> GetProductsDtoAsync(IEnumerable<string> productIds);

        Task<IEnumerable<ProductDto>> GetProductsDtoAsync();

        Task<IEnumerable<ProductDto>> GetProductsDtoByCategoryAsync(string categoryId);

        Task<long> GetCountAsync();

        Task<AddProductDto> CreateOneAsync(AddProductDto addProductDto);

        Task<IEnumerable<ProductDto>> CreateManyAsync(IEnumerable<ProductDto> products);

        Task<bool> DeleteAsync(string productId);

        Task<bool> IsValidProductIdAsync(string productId);

        Task<IEnumerable<ProductDto>> GetShoppingCartProductsDto(ShoppingCart shoppingCart);
        //

        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Product>> GetProductsAsync(IEnumerable<string> productIds);
        Task<Product> GetProductAsync(string productId);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryId);

    }
}
