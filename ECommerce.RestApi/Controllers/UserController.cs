using ECommerce.RestApi.Models;
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
        public async Task<ActionResult<User>> Get(string id)
        {
            var user = await _userService.GetUserAsync(id);
            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDTO userDTO)
        {
            var isExist = await _userService.IsExistingUser(userDTO.Username);
            if (isExist)
            {
                return BadRequest("This username is already exist.");
            }

            User user = new User
            {
                Username = userDTO.Username,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Password = userDTO.Password
            };

            await _userService.CreateOneAsync(user);

            return Ok(user);
        }
        
    }
}
