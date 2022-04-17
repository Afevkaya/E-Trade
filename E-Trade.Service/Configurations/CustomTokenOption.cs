using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Trade.Service.Configurations
{
    // Token oluştururken gerekli olan parametreler.
    // Bu parametrelere erişebilmek için parametrelere karşılık gelen property'lerin bulunduğu class.

    // CustomTokenOption class
    public class CustomTokenOption
    {
        public List<string> Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
