using E_Trade.Core.DTOs;
using E_Trade.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Trade.API.Controllers
{
    
    public class UsersController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            var result = await _userService.CreateUserAsync(createUserDto);
            return CreatActionResult(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            // Name Token'dan gelecek.
            var result = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
            return CreatActionResult(result);
        }

    }
}
