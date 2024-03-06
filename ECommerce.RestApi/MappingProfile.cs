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
            CreateMap<Category, CategoryDto>();

            CreateMap<Product, ProductDto>();
            CreateMap<Product, CreateProductDto>();

            CreateMap<User, CreateUserDto>();
            CreateMap<User, UserSummaryDto>()
                .ForMember(dest => dest.FavoritesCount, opt => 
                    opt.MapFrom(src => GetFavoritesCount(src.Favorites)))
                .ForMember(dest => dest.ShoppingCartItemsCount, opt => 
                    opt.MapFrom(src => src.Cart.TotalItemCount));


            //Projections
            CreateProjection<User, UserSummaryDto>()
                .ForMember(dest => dest.FavoritesCount, opt =>
                    opt.MapFrom(src => GetFavoritesCount(src.Favorites)))
                .ForMember(dest => dest.ShoppingCartItemsCount, opt =>
                    opt.MapFrom(src => src.Cart.TotalItemCount));

            CreateProjection<Category, CategoryDto>();
        }

        private int GetFavoritesCount(List<string> favorites)
        {
            return favorites?.Count ?? 0;
        }
    }
}
