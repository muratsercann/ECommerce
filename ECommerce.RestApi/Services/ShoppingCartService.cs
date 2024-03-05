using AutoMapper;
using ECommerce.RestApi.Models;
using ECommerce.RestApi.Models.DTOs;
using MongoDB.Driver;

namespace ECommerce.RestApi.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ECommerceContext _mongoContext;
        private readonly IMapper _mapper;

        public ShoppingCartService(ECommerceContext mongoContext, IMapper mapper)
        {
            _mongoContext = mongoContext;
            _mapper = mapper;
        }

        private async Task<User> GetUserAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            var user = await _mongoContext.Users.Find(filter).FirstOrDefaultAsync();
            return user;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsInTheCartAsync(string userId)
        {
            var user = await GetUserAsync(userId);

            if (user?.Cart?.Items is null || user.Cart.Items.Count == 0)
            {
                return new List<ProductDto>();
            }

            var filter = Builders<Product>.Filter.In(p => p.Id, user.Cart.Items.Select(f => f.ProductId));
            var products = await _mongoContext.Products.Find(filter).ToListAsync();
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }

        public async Task<ShoppingCartDto> GetShoppingCartDtoAsync(string userId)
        {
            var user = await GetUserAsync(userId);

            if (user is null || (user.Cart?.Items?.Count ?? 0) == 0)
            {
                return new ShoppingCartDto();
            }

            List<string> productIds = user.Cart.Items.Select(i => i.ProductId).ToList();

            var filter = Builders<Product>.Filter.In(p => p.Id, productIds);
            var products = await _mongoContext.Products.Find(filter).ToListAsync();

            IEnumerable<ProductDto> productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            decimal totalPrice = 0;

            List<ShoppingCartItemDto> items = new List<ShoppingCartItemDto>();

            foreach (var item in user.Cart.Items)
            {
                ProductDto productDto = productDtos.Where(p => p.Id == item.ProductId).FirstOrDefault();
                totalPrice += (productDto?.Price ?? 0) * item.Quantity;
                items.Add(new ShoppingCartItemDto(productDto, item.Quantity));
            }

            ShoppingCartDto shoppingCartDto = new ShoppingCartDto(items, totalPrice, user.Cart.TotalItemCount);

            return shoppingCartDto;
        }

        public async Task<ShoppingCart> UpdateCartAsync(string userId, ShoppingCart cart)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var update = Builders<User>.Update.Set(u => u.Cart, cart);
            var result = await _mongoContext.Users.UpdateOneAsync(filter, update);

            return cart;
        }

        public async Task<ShoppingCart> GetUserCart(string userId)
        {
            var user = await GetUserAsync(userId);

            if (user.Cart is null || user.Cart.Items == null || user.Cart.Items.Count == 0)
            {
                return new ShoppingCart();//???
            }
            var productIds = user.Cart.Items.Select(i => i.ProductId).ToList();
            var filter = Builders<Product>.Filter.In(p => p.Id, productIds);
            var cartProducts = await _mongoContext.Products.Find(filter).ToListAsync();
            var totalPrice = cartProducts.Sum(p => p.Price * GetCartItemCount(user.Cart, p.Id));
            user.Cart.TotalPrice = totalPrice;

            return user.Cart;

            int GetCartItemCount(ShoppingCart cart, string productId)
            {
                var item = cart.Items.Where(i => i.ProductId == productId).FirstOrDefault();
                return item?.Quantity ?? 0;
            }


        }

        public async Task<bool> AddToCartAsync(AddToCartDto addToCartObject)
        {
            var user = await GetUserAsync(addToCartObject.userId);

            if (user is null)
            {
                return false;
            }

            var cartItem = user.Cart.Items.Where(item => item.ProductId == addToCartObject.productId).FirstOrDefault();

            if (cartItem is not null)
            {
                cartItem.Quantity += addToCartObject.quantity;
            }

            else
            {
                user.Cart.Items.Add(new ShoppingCartItem
                {
                    ProductId = addToCartObject.productId,
                    Quantity = addToCartObject.quantity,
                });
            }

            user.Cart.TotalItemCount = user.Cart.Items.Sum(item => item.Quantity);

            await UpdateCartAsync(addToCartObject.userId, user.Cart);

            var updatedUser = await GetUserAsync(addToCartObject.userId);

            return true;
        }

        public async Task<bool> RemoveFromCartAsync(AddToCartDto cartDto)
        {
            var user = await GetUserAsync(cartDto.userId);

            if (user is null)
            {
                return false;
            }

            if (user.Cart?.Items is null || user.Cart.Items.Count == 0)
            {
                return false;
            }

            var item = user.Cart.Items.Where(item => item.ProductId == cartDto.productId).FirstOrDefault();

            if (item is null)
            {
                return false;
            }

            if (item.Quantity > cartDto.quantity)
            {
                item.Quantity -= cartDto.quantity;
            }
            else
            {
                user.Cart.Items.Remove(item);
            }

            user.Cart.TotalItemCount = user.Cart.Items.Sum(item => item.Quantity);

            await UpdateCartAsync(cartDto.userId, user.Cart);

            var updatedUser = await GetUserAsync(cartDto.userId);

            return true;
        }


        public async Task<bool> RemoveAllFromCartAsync(string userId)
        {
            var user = await GetUserAsync(userId);

            if (user is null)
            {
                return false;
            }

            if (user.Cart?.Items is null || user.Cart.Items.Count == 0)
            {
                return false;
            }

            user.Cart.Items = new List<ShoppingCartItem>();

            await UpdateCartAsync(userId, user.Cart);

            return true;
        }
    }
}
