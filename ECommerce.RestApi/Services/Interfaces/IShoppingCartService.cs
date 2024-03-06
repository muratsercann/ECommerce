using ECommerce.RestApi.Models;
using ECommerce.RestApi.Dto;

namespace ECommerce.RestApi.Services
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto> GetShoppingCartDtoAsync(string userId);

        Task<IEnumerable<ProductDto>> GetProductsInTheCartAsync(string userId);

        Task<ShoppingCart> UpdateCartAsync(string userId, ShoppingCart cart);

        Task<ShoppingCart> GetUserCart(string userId);

        Task<bool> AddToCartAsync(AddToCartDto cartDto);

        Task<bool> RemoveFromCartAsync(AddToCartDto cartDto);
        Task<bool> RemoveAllFromCartAsync(string userId);
    }
}
