using E_Trade.Core.DTOs;
using E_Trade.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Trade.API.Controllers
{
    public class AuthsController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthsController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService; 
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result = await _authenticationService.CreateTokenAsync(loginDto);
            return CreatActionResult(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.CreateTokenByRefreshTokenAsync(refreshTokenDto.Token);
            return CreatActionResult(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RevokeToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.RevokeRefreshToken(refreshTokenDto.Token);
            return CreatActionResult(result);
        }

    }
}
