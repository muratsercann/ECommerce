using AutoMapper;
using ECommerce.RestApi.Models;
using ECommerce.RestApi.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;
using System.Formats.Asn1;

namespace ECommerce.RestApi.Services
{
    public class UserService : IUserService
    {
        private readonly ECommerceContext _mongoContext;
        private readonly IMapper _mapper;

        public UserService(ECommerceContext mongoContext, IMapper mapper)
        {
            _mongoContext = mongoContext;
            _mapper = mapper;
        }
        public async Task<User> CreateAsync(User user)
        {
            await _mongoContext.Users.InsertOneAsync(user);
            return user;
        }

        public async Task<List<User>> CreateManyAsync(List<User> users)
        {
            await _mongoContext.Users.InsertManyAsync(users);
            return users;
        }

        public Task<bool> DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _mongoContext.Users.AsQueryable().ToListAsync();
        }

        public async Task<User> GetUserAsync(string userId)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var user = await _mongoContext.Users.Find(filter).FirstOrDefaultAsync();
            return user;
        }

        public async Task<long> GetUsersCountAsync()
        {
            return await _mongoContext.Users.CountDocumentsAsync(FilterDefinition<User>.Empty);
        }

        public async Task<bool> IsExistingUser(string username)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);
            var result = await _mongoContext.Users.Find(filter).FirstOrDefaultAsync();

            return result is null ? false : true;
        }

        public async Task<UserSummaryDto> GetUserSummaryDtoAsync(string userId)
        {

            //msercan?: Burda user bilgisini çekip automapper ile mi maplemek lazım.
            //Yoksa bu şekilde projection ile almak mı ?
            var user = await _mongoContext.Users
              .Find(u => u.Id == userId)
              .Project(u => new UserSummaryDto
              {
                  Username = u.Username,
                  FirstName = u.FirstName,
                  LastName = u.LastName,
                  Email = u.Email,
                  FavoritesCount = u.Favorites.Count,
                  ShoppingCartItemsCount = u.Cart != null ? u.Cart.Items.Sum(i => i.Quantity) : 0,
              }).FirstOrDefaultAsync();

            return user;
        }

        public async Task<UserDetailDto> GetUserDetailDtoAsync(string userId)
        {
            //msercan??
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var user = await _mongoContext.Users.Find(filter).FirstOrDefaultAsync();

            IEnumerable<ProductDto> favorites = await GetFavoriteProductsDtoAsync(userId);
            ShoppingCartDto shoppingCartDto = await GetShoppingCartDtoAsync(userId);

            var userDetailDto = new UserDetailDto
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Addresses = user.Addresses,
                Favorites = favorites,
                ShoppingCart = shoppingCartDto
            };

            return userDetailDto;
        }

        private async Task<IEnumerable<ProductDto>> GetFavoriteProductsDtoAsync(string userId)
        {
            //aynı metod FavoriteService içerisinde de var
            var favorites = await GetFavoriteIds(userId);
            var products = await GetProductsDtoAsync(favorites);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }

        private async Task<IEnumerable<string>> GetFavoriteIds(string userId)
        {
            //aynı metod FavoriteService içerisinde de var
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(user => user.Id, userId);

            List<string> favoriteIds = await _mongoContext.Users
              .Find(u => u.Id == userId)
              .Project(u =>
                  u.Favorites
              ).FirstOrDefaultAsync();

            return favoriteIds;
        }

        private async Task<ShoppingCartDto> GetShoppingCartDtoAsync(string userId)
        {
            var user = await GetUserAsync(userId);

            if (user is null || (user.Cart?.Items?.Count ?? 0) == 0)
            {
                return new ShoppingCartDto();
            }

            List<string> productIds = user.Cart.Items.Select(i => i.ProductId).ToList();
            
            IEnumerable<ProductDto> productDtos = await GetProductsDtoAsync(productIds);

            decimal totalPrice = 0;

            List<ShoppingCartItemDto> items = new List<ShoppingCartItemDto>();

            foreach (var item in user.Cart.Items)
            {
                ProductDto? productDto = productDtos.Where(p => p.Id == item.ProductId).FirstOrDefault();

                if (productDto is not null)
                {
                    totalPrice += productDto.Price * item.Quantity;
                    items.Add(new ShoppingCartItemDto(productDto, item.Quantity));
                }
            }

            ShoppingCartDto shoppingCartDto = new ShoppingCartDto(items, totalPrice, user.Cart.TotalItemCount);

            return shoppingCartDto;
        }

        private async Task<IEnumerable<ProductDto>> GetProductsDtoAsync(IEnumerable<string> productIds)
        {
            var filter = Builders<Product>.Filter.In(u => u.Id, productIds);
            var products = await _mongoContext.Products.Find(filter).ToListAsync();
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }

    }
}
