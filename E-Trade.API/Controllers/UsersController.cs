using E_Trade.Core.DTOs;
using E_Trade.Core.Services;
using Microsoft.AspNetCore.Authorization;
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

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var appUserDto = await _userService.GetUsersAsync();
            return CreatActionResult(appUserDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            var result = await _userService.CreateUserAsync(createUserDto);
            return CreatActionResult(result);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserByUserName()
        {
            // Name Token'dan gelecek.
            var result = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
            return CreatActionResult(result);
        }


    }
}
