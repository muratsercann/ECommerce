using ECommerce.RestApi.Models;
using ECommerce.RestApi.Dto;

namespace ECommerce.RestApi.Services
{

    public interface IUserService
    {
        Task<User> GetAsync(string userId);

        Task<IEnumerable<User>> GetAllAsync();

        Task<UserSummaryDto> GetUserSummaryDtoAsync(string userId);

        Task<bool> AddAsync(CreateUserDto userDto);

        Task<bool> DeleteAsync(string userId);

        Task<bool> ExistsUserName(string username);

        Task<long> GetCountAsync();

                
    }
}
