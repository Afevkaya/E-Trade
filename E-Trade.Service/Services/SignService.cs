using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Trade.Service.Services
{
    // Secret Key imzalama(şifreleme) class'ı
    public static class SignService
    {
        // Simetrik imzalama(şifreleme) metod
        public static SecurityKey GetSymmetricSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }

    }
}
