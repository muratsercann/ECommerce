﻿using AutoMapper;
using ECommerce.RestApi.Models;
using ECommerce.RestApi.Models.DTOs;
using ECommerce.RestApi.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;
using System.Formats.Asn1;

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

        public async Task<User> GetUserAsync(string userId)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var user = await _userRepository.GetByIdAsync(userId);
            return user;
        }

        public async Task<UserSummaryDto> GetUserSummaryDtoAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync<UserSummaryDto>(userId, UserSummaryDto.Selector);
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<bool> AddAsync(CreateUserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            bool result = await _userRepository.AddAsync(user);
            return result;
        }

        public async Task<long> GetUsersCountAsync()
        {
            return await _userRepository.GetCountAsync();
        }

        public async Task<bool> IsExistingUser(string id)
        {
            return await _userRepository.ExistsAsync(id);
        }
       
    }
}
