using AutoMapper;
using ECommerce.RestApi.Models;
using ECommerce.RestApi.Dto;
using ECommerce.RestApi.Services;
using MongoDB.Driver;

namespace ECommerce.RestApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Mappings
            CreateMap<Category, CategoryDto>();

            CreateMap<Product, ProductDto>();
            CreateMap<Product, CreateProductDto>();
            CreateMap<Product, ProductSummaryDto>();

            CreateMap<User, CreateUserDto>();
            CreateMap<User, UserSummaryDto>()
                .ForMember(dest => dest.FavoritesCount, opt => 
                    opt.MapFrom(src => GetFavoritesCount(src.Favorites)))
                .ForMember(dest => dest.ShoppingCartItemsCount, opt => 
                    opt.MapFrom(src => src.Cart.TotalItemQuantity));


            //Projections
            CreateProjection<User, UserSummaryDto>()
                .ForMember(dest => dest.FavoritesCount, opt =>
                    opt.MapFrom(src => GetFavoritesCount(src.Favorites)))
                .ForMember(dest => dest.ShoppingCartItemsCount, opt =>
                    opt.MapFrom(src => src.Cart.TotalItemQuantity));

            CreateProjection<Category, CategoryDto>();
        }

        private int GetFavoritesCount(List<string> favorites)
        {
            return favorites?.Count ?? 0;
        }
    }
}
