using ECommerce.RestApi.Models;
using MongoDB.Driver;

namespace ECommerce.RestApi.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ECommerceContext _eCommerceContext;

        public CartItemService(ECommerceContext eCommerceContext)
        {
            _eCommerceContext = eCommerceContext;
        }
        public async Task<CartItem> CreateAsync(string cartId, string productId, int quantity)
        {
            var builder = Builders<CartItem>.Filter;
            var filterById = Builders<CartItem>.Filter.Eq(c => c.CartId, cartId);
            var filterByProductId = Builders<CartItem>.Filter.Eq(c => c.ProductId, productId);
            var completeFilter = builder.And(filterById, filterByProductId);


            CartItem cartItem = await _eCommerceContext.CartItems.Find(completeFilter).FirstOrDefaultAsync(); ;

            if (cartItem != null)
            {
                var newQuantity = cartItem.Quantity + quantity;
                var update = Builders<CartItem>.Update.Set(c => c.Quantity, newQuantity);

                await _eCommerceContext.CartItems.UpdateOneAsync(completeFilter, update);
            }
            else
            {
                cartItem = new CartItem()
                {
                    CartId = cartId,
                    ProductId = productId,
                    Quantity = quantity,
                };

                await _eCommerceContext.CartItems.InsertOneAsync(cartItem);
            }

            return cartItem;
        }

        public Task<CartItem> DeleteAsync(CartItem item, string cartId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CartItem>> GetCartItemsAsync(string cartId)
        {
            return await _eCommerceContext.CartItems.Find(Builders<CartItem>.Filter.Eq(c => c.CartId, cartId)).ToListAsync();
        }

        
    }
}
