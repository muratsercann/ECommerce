using ECommerce.RestApi.Models;

namespace ECommerce.RestApi.Services
{

    public interface IUserService
    {
        Task<User> GetUserAsync(string username);

        Task<List<User>> GetAllUserAsync();

        Task<long> GetUsersCountAsync();

        Task<User> CreateOneAsync(User user);

        Task<List<User>> CreateManyAsync(List<User> users);

        Task<User> UpdateAsync(User user);

        Task<bool> DeleteAsync(User user);
    }
}
