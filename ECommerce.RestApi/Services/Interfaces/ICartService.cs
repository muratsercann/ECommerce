using ECommerce.RestApi.DTOs;
using ECommerce.RestApi.Models;
using System.Runtime.InteropServices;

namespace ECommerce.RestApi.Services
{
    public interface ICartService
    {
        Task<List<CartItemDTO>> GetProductsInfo(string cartId);

        public Task<Cart> GetCart(string userId);

        public Task<Cart> CreateEmptyCart(string userId);

        public Task<Cart> CreateCart(Cart cart);

        public Task<Cart> RemoveItem(Cart cart, string cartItemId);

        public bool Clear(string userId);


    }
}
