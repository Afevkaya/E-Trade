using E_Trade.Core.DTOs;

namespace E_Trade.Core.Services
{
    // User Authentication işlemleri için oluşturulan interface

    // IAuthenticationService interface
    public interface IAuthenticationService
    {
        Task<CustomResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto);
        Task<CustomResponseDto<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken);
        Task<CustomResponseDto<NoContentDto>> RevokeRefreshToken(string refreshToken);
    }
}
