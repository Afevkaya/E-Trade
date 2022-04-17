using E_Trade.Core.DTOs;
using E_Trade.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Trade.Core.Services
{
    // Token üretecek service class'ın implement edeceği interface
    // Proje içinde kullanılacak.

    // ITokenService interface
    public interface ITokenService
    {
        TokenDto CreateToken(AppUser appUser);
    }
}
