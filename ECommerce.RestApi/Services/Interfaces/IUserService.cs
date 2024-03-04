using ECommerce.RestApi.Models;
using ECommerce.RestApi.Models.DTOs;
using MongoDB.Bson;

namespace ECommerce.RestApi.Services
{

    public interface IUserService
    {
        Task<User> GetUserAsync(string userId);

        Task<UserSummaryDto> GetUserSummaryDtoAsync(string userId);

        Task<UserDetailDto> GetUserDetailDtoAsync(string userId);

        Task<List<User>> GetAllUserAsync();

        Task<bool> IsExistingUser(string username);//msercan : username ??

        Task<long> GetUsersCountAsync();

        Task<User> CreateAsync(User user);

        Task<List<User>> CreateManyAsync(List<User> users);

        Task<bool> DeleteAsync(User user);
                
    }
}
