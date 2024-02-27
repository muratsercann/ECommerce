using ECommerce.RestApi.Models;
using MongoDB.Driver;

namespace ECommerce.RestApi.Services
{
    public class UserService : IUserService
    {
        private readonly ECommerceContext _mongoContext;

        public UserService(ECommerceContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task<List<User>> CreateManyAsync(List<User> users)
        {
            await _mongoContext.Users.InsertManyAsync(users);
            return users;
        }

        public async Task<User> CreateOneAsync(User user)
        {
            await _mongoContext.Users.InsertOneAsync(user);
            return user;
        }

        public Task<bool> DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _mongoContext.Users.AsQueryable().ToListAsync();
        }

        public Task<User> GetUserAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<long> GetUsersCountAsync()
        {
            return await _mongoContext.Users.CountDocumentsAsync(FilterDefinition<User>.Empty);
        }

        public Task<User> UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
