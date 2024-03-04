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
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public ShoppingCartService(ECommerceContext mongoContext, IMapper mapper, IUserService userService, IProductService productService)
        {
            _mongoContext = mongoContext;
            _mapper = mapper;
            _userService = userService;
            _productService = productService;
        }
        public async Task<IEnumerable<ProductDto>> GetProductsInTheCartAsync(string userId)
        {
            var user = await _userService.GetUserAsync(userId);

            if (user?.Cart?.Items is null || user.Cart.Items.Count == 0)
            {
                return new List<ProductDto>();
            }

            var filter = Builders<Product>.Filter.In(p => p.Id, user.Cart.Items.Select(f => f.ProductId));
            var products = await _mongoContext.Products.Find(filter).ToListAsync();
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }

        public Task<ShoppingCartDto> GetShoppingCartDtoAsync(string userId)
        {
            throw new NotImplementedException();
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
            var user = await _userService.GetUserAsync(userId);

            if (user.Cart is null || user.Cart.Items == null || user.Cart.Items.Count == 0)
            {
                return new ShoppingCart();//???
            }

            var productIds = user.Cart.Items.Select(i => i.ProductId).ToList();
            var cartProducts = await _productService.GetProductsAsync(productIds);
            var totalPrice = cartProducts.Sum(p => p.Price * GetCartItemCount(user.Cart, p.Id));
            user.Cart.TotalPrice = totalPrice;

            return user.Cart;

            int GetCartItemCount(ShoppingCart cart, string productId)
            {
                var item = cart.Items.Where(i => i.ProductId == productId).FirstOrDefault();
                return item?.Quantity ?? 0;
            }


        }

        public Task<bool> AddToCart(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFromCart(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
