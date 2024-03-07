using AutoMapper;
using ECommerce.RestApi.Models;
using ECommerce.RestApi.Dto;
using ECommerce.RestApi.Repositories;

namespace ECommerce.RestApi.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<User> GetAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user;
        }

        public async Task<UserSummaryDto> GetUserSummaryDtoAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId, UserSummaryDto.Selector);
            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<bool> AddAsync(CreateUserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            bool result = await _userRepository.AddAsync(user);
            return result;
        }

        public async Task<long> GetCountAsync()
        {
            return await _userRepository.GetCountAsync();
        }

        public async Task<bool> ExistsUserNameAsync(string id)
        {
            return await _userRepository.ExistsAsync(id);
        }

        public async Task<bool> DeleteAsync(string userId)
        {
            return await _userRepository.DeleteAsync(userId);
        }
    }
}
