using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Trade.Core.Models
{
    // Refresh Token'a ait olması gereken bilgiler bulunmaktadır.
    // Refresh Token Database' kayıt edilecek.

    // UserRefreshToken entity class
    public class UserRefreshToken
    {
        public string UserId { get; set; }
        public string Code { get; set; }
        public DateTime Expiration { get; set; }

    }
}
