using ECommerce.RestApi.Dto;
using ECommerce.RestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _userService.GetAsync(id);
            return Ok(user);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetUserSummary(string id)
        {
            var user = await _userService.GetUserSummaryDtoAsync(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDto userDto)
        {
            bool result = await _userService.AddAsync(userDto);
            return Ok(result);
        }        
    }
}
