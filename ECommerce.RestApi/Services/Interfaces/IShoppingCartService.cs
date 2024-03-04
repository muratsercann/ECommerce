using ECommerce.RestApi.Models;
using ECommerce.RestApi.Models.DTOs;

namespace ECommerce.RestApi.Services
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto> GetShoppingCartDtoAsync(string userId);

        Task<IEnumerable<ProductDto>> GetProductsInTheCartAsync(string userId);

        Task<ShoppingCart> UpdateCartAsync(string userId, ShoppingCart cart);

        Task<ShoppingCart> GetUserCart(string userId);

        Task<bool> AddToCart(string userId);

        Task<bool> RemoveFromCart(string userId);
    }
}
