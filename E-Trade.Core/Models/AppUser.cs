using Microsoft.AspNetCore.Identity;

namespace E_Trade.Core.Models
{
    // User' ait bilgiler IdentityUser class'ından hazır olarak gelmektedir.
    // Bu özelliklere fazladan özellik eklemek istenilirse AppUser class'ı içine eklenir.

    // User entity class
    public class AppUser : IdentityUser
    {
    }
}
