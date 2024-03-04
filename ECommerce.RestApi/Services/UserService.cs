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
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public UserService(ECommerceContext mongoContext, IProductService productService, IMapper mapper)
        {
            _mongoContext = mongoContext;
            _productService = productService;
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
                  ShoppingCarItemsCount = u.Cart.Items.Count,
              }).FirstOrDefaultAsync();

            return user;
        }

        public async Task<UserDetailDto> GetUserDetailDtoAsync(string userId)
        {
            //msercan todo : fix this method
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var user = await _mongoContext.Users.Find(filter).FirstOrDefaultAsync();



            var userDetailDto = _mapper.Map<UserDetailDto>(user);
            return userDetailDto;
        }
    }
}
