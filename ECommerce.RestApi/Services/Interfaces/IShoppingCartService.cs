using ECommerce.RestApi.Models;
using ECommerce.RestApi.Dto;

namespace ECommerce.RestApi.Services
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto> GetShoppingCartAsync(string userId);

        Task<bool> AddToCartAsync(AddToCartDto cartDto);

        Task<bool> RemoveFromCartAsync(AddToCartDto cartDto);

        Task<bool> RemoveAllFromCartAsync(string userId);
    }
}
