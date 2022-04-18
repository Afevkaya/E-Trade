using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Trade.Service.Services
{
    // Access Token 'ı imzalamak için gerekli string ifadeyi imzalayan class. 

    // SignService class
    public static class SignService
    {
        // Simetrik imzalama metodu.
        public static SecurityKey GetSymmetricSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
