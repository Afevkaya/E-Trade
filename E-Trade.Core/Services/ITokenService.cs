using E_Trade.Core.DTOs;
using E_Trade.Core.Models;

namespace E_Trade.Core.Services
{
    // Token üretecek/oluşturacak interface.
    // ITokenService interface proje içinde kullanılcak.
    // Bu sebepten kullanıcı ile direkt olarak etkileşime girmeyecek.

    // ITokenService interface
    public interface ITokenService
    {
        TokenDto CreateToken(AppUser appUser);
    }
}
