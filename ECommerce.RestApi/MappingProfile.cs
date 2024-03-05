using AutoMapper;
using ECommerce.RestApi.Models;
using ECommerce.RestApi.Models.DTOs;
using ECommerce.RestApi.Services;
using MongoDB.Driver;

namespace ECommerce.RestApi
{
    public class MappingProfile : Profile
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Product, AddProductDto>();
            CreateMap<User, CreateUserDto>();
            CreateMap<User, UserSummaryDto>()
                .ForMember(dest => dest.FavoritesCount, opt => opt.MapFrom(src => GetFavoritesCount(src.Favorites)))
                .ForMember(dest => dest.ShoppingCartItemsCount, opt => opt.MapFrom(src => GetShoppingCartItemsCount(src.Cart)));

            //CreateMap<User, UserDetailDto>()
            //.ForMember(dest => dest.Favorites, opt => opt.MapFrom(src => GetFavoriteProducts(src.Favorites)))
            //.ForMember(dest => dest.ShoppingCart, opt => opt.MapFrom(src => GetShoppingCartDto(src.Cart)));

        }

        private object GetShoppingCartItemsCount(ShoppingCart cart)
        {
            return cart?.Items?.Count ?? 0;
        }


        private int GetFavoritesCount(List<string> favorites)
        {
            return favorites?.Count ?? 0;
        }

        public static IEnumerable<ProductDto> GetFavoriteProducts(List<string> favoriteIds)
        {
            var productDtos = favoriteIds.Select(x => new ProductDto
            {
                Id = $"id_{x}",
                Name = $"name_{x}",
                Price = 45,
                Description = $"desc_{x}"
            });
            return productDtos;
        }


        public static ShoppingCartDto GetShoppingCartDto(ShoppingCart cart)
        {
            if ((cart?.Items?.Count ?? 0) == 0)
            {
                return new ShoppingCartDto();
            }

            var items = new List<ShoppingCartItemDto>();

            foreach (var x in cart.Items)
            {
                items.Add(new ShoppingCartItemDto(
                   new ProductDto
                   {
                       Id = $"id_{x.ProductId}",
                       Name = $"name_{x.ProductId}",
                       Price = 45,
                       Description = $"desc_{x.ProductId}"

                   },
                   x.Quantity
                ));
            }

            var cartDto = new ShoppingCartDto(items, 0, 0);

            return cartDto;
        }

        private async Task<IEnumerable<ProductDto>> MapFavoriteProducts(List<string> favoriteProductIds)
        {
            var productsDto = await _productService.GetProductsDtoAsync(favoriteProductIds);
            return productsDto;
        }

        private async Task<ShoppingCartDto> MapShoppingCart(string userId, ShoppingCart shoppingCart)
        {

            IEnumerable<ProductDto> productsDtoInTheCart = await _productService.GetShoppingCartProductsDto(shoppingCart);

            List<ShoppingCartItemDto> cartItemDtoList = new List<ShoppingCartItemDto>();
            decimal totalPrice = 0;
            int totalItemCount = 0;
            foreach (var item in productsDtoInTheCart)
            {
                var quantity = shoppingCart!.Items.Where(i => i.ProductId == item.Id).FirstOrDefault()?.Quantity ?? 0;
                totalItemCount += quantity;
                totalPrice += (quantity * item.Price);
                cartItemDtoList.Add(new ShoppingCartItemDto(item, quantity));
            }

            ShoppingCartDto shoppingCartDto = new ShoppingCartDto(cartItemDtoList, totalPrice, totalItemCount);

            return shoppingCartDto;
        }

    }
}
