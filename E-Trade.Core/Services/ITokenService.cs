using E_Trade.Core.DTOs;
using E_Trade.Core.Models;

namespace E_Trade.Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(AppUser appUser);
    }
}
