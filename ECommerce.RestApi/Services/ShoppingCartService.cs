using AutoMapper;
using ECommerce.RestApi.Models;
using ECommerce.RestApi.Dto;
using MongoDB.Driver;
using ECommerce.RestApi.Repositories;

namespace ECommerce.RestApi.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<ShoppingCartDto> GetShoppingCartAsync(string userId)
        {
            var cart = await _shoppingCartRepository.GetShoppingCartAsync(userId);

            if ((cart?.Items?.Count ?? 0) == 0)
            {
                //or return message
                return new ShoppingCartDto();
            }

            IEnumerable<string> productIds = cart.Items.Select(item => item.ProductId);
            IEnumerable<ProductDto> products = await _shoppingCartRepository.GetProductsAsync(productIds, ProductDto.Selector);

            var mergedList = cart.Items
            .Join(products,
                item => item.ProductId,
                product => product.Id,
                (item, product) => new ShoppingCartItemDto(product, item.Quantity, item.Quantity * product.Price))
            .ToList();

            var totalPrice = mergedList.Sum(m => m.price);

            ShoppingCartDto cartDto = new ShoppingCartDto(
                mergedList,
                totalPrice,
                cart.TotalItemQuantity);

            return cartDto;
        }

        public async Task<bool> AddToCartAsync(AddToCartDto cartDto)
        {
            //msercan? : refactor..
            var userCart = await _shoppingCartRepository.GetShoppingCartAsync(cartDto.userId);

            if (userCart is null)
            {
                //msercan : return message..
                return false;
            }

            var cartItem = userCart.Items.Where(item => item.ProductId == cartDto.productId).FirstOrDefault();

            if (cartItem is not null)
            {
                cartItem.Quantity += cartDto.quantity;
            }

            else
            {
                userCart.Items.Add(new ShoppingCartItem
                {
                    ProductId = cartDto.productId,
                    Quantity = cartDto.quantity,
                });

            }

            userCart.TotalItemQuantity = userCart.Items.Sum(item => item.Quantity);

            await _shoppingCartRepository.UpdateShoppingCartAsync(cartDto.userId, userCart);

            return true;
        }

        public async Task<bool> RemoveFromCartAsync(AddToCartDto cartDto)
        {
            //msercan? : refactor..
            var userCart = await _shoppingCartRepository.GetShoppingCartAsync(cartDto.userId);

            if (userCart is null)
            {
                return false;
            }

            if (userCart?.Items is null || userCart.Items.Count == 0)
            {
                return false;
            }

            var item = userCart.Items.Where(item => item.ProductId == cartDto.productId).FirstOrDefault();

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
                userCart.Items.Remove(item);
            }

            userCart.TotalItemQuantity = userCart.Items.Sum(item => item.Quantity);

            var result = await _shoppingCartRepository.UpdateShoppingCartAsync(cartDto.userId, userCart);

            return result;
        }

        public async Task<bool> RemoveAllFromCartAsync(string userId)
        {
            //msercan? : refactor..
            var userCart = await _shoppingCartRepository.GetShoppingCartAsync(userId);

            if (userCart is null)
            {
                return false;
            }

            if (userCart?.Items is null || userCart.Items.Count == 0)
            {
                return false;
            }

            userCart.Items = new List<ShoppingCartItem>();
            userCart.TotalItemQuantity = 0;

            var result = await _shoppingCartRepository.UpdateShoppingCartAsync(userId, userCart);

            return result;
        }
    }
}
