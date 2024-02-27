using ECommerce.RestApi.Models;
using MongoDB.Driver;

namespace ECommerce.RestApi.Services
{
    public interface ICartItemService
    {
        public Task<List<CartItem>> GetCartItemsAsync(string userId);

        public Task<CartItem> CreateAsync(string cartId, string productId, int quantity);

        public Task<CartItem> DeleteAsync(CartItem item, string cartId);

    }
}
