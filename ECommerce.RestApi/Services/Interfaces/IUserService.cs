using ECommerce.RestApi.Models;
using ECommerce.RestApi.Models.DTOs;
using MongoDB.Bson;

namespace ECommerce.RestApi.Services
{

    public interface IUserService
    {
        Task<User> GetUserAsync(string userId);

        Task<IEnumerable<User>> GetAllUserAsync();

        Task<UserSummaryDto> GetUserSummaryDtoAsync(string userId);

        Task<bool> AddAsync(CreateUserDto userDto);

        Task<bool> IsExistingUser(string username);//msercan : username ??

        Task<long> GetUsersCountAsync();

                
    }
}
