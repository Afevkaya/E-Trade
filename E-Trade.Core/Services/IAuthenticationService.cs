using E_Trade.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Trade.Core.Services
{
    // IAuthenticationService interface
    public interface IAuthenticationService
    {
        Task<CustomResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto);
        Task<CustomResponseDto<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken);
        Task<CustomResponseDto<NoContentDto>> RevokeRefreshToken(TokenDto tokenDto);
    }
}
