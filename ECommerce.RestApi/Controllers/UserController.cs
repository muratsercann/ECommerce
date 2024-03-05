﻿using ECommerce.RestApi.Models;
using ECommerce.RestApi.Models.DTOs;
using ECommerce.RestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _userService.GetUserAsync(id);
            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetUserSummary(string id)
        {
            var user = await _userService.GetUserSummaryDtoAsync(id);
            
            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDto userDto)
        {
            var isExist = await _userService.IsExistingUser(userDto.Username);
            if (isExist)
            {
                return BadRequest("This username is already exist.");
            }

            bool result = await _userService.AddAsync(userDto);

            return Ok(result);
        }

        //msercan : update...
        
    }
}
