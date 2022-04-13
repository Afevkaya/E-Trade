using E_Trade.Core.DTOs;

namespace E_Trade.Core.Services
{
    public interface IAuthenticationService
    {
        Task<CustomResponseDto<TokenDto>> CreateTokenAsyn(LoginDto loginDto);
        Task<CustomResponseDto<TokenDto>> CreateTokenAsyncByRefreshToken(string refreshToken);
        Task<CustomResponseDto<NoContentDto>> RevokeRefreshtoken(string refreshToken);
    }
}
