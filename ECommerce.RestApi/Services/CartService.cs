using ECommerce.RestApi.DTOs;
using ECommerce.RestApi.Models;
using MongoDB.Driver;

namespace ECommerce.RestApi.Services
{
    public class CartService : ICartService
    {
        private readonly ECommerceContext _eCommerceContext;

        public CartService(ECommerceContext eCommerceContext)
        {
            _eCommerceContext = eCommerceContext;
        }

        public async Task<List<CartItemDTO>> GetProductsInfo(string cartId)
        {
            var cartItems = await _eCommerceContext.CartItems
            .Find(c => c.CartId == cartId)
            //.Project(c => new { c.ProductId, c.Quantity })
            .ToListAsync();

            var cartItemIds = cartItems.Select(c => c.ProductId);
            var filter = Builders<Product>.Filter.In(p => p.Id, cartItemIds);
            var userCartProducts = await _eCommerceContext.Products.Find(filter).ToListAsync();


            var cartItemDTOs = userCartProducts.Select(product =>
            {
                var quantity = cartItems.FirstOrDefault(c => c.ProductId == product.Id)?.Quantity ?? 0;
                var price = quantity * product.Price;
                return new CartItemDTO(product, quantity,price);
            })
        .ToList();

            return cartItemDTOs;
        }


        public bool Clear(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> CreateCart(Cart cart)
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> CreateEmptyCart(string userId)
        {
            var cart = new Cart { UserId = userId };
            await _eCommerceContext.Carts.InsertOneAsync(cart);
            return await GetCart(userId);
        }

        public async Task<Cart> GetCart(string userId)
        {
            return await _eCommerceContext.Carts.Find(Builders<Cart>.Filter.Eq(c => c.UserId, userId)).FirstOrDefaultAsync();
        }

        public Task<Cart> RemoveItem(Cart cart, string cartItemId)
        {
            throw new NotImplementedException();
        }

    }
}
