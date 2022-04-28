 namespace E_Trade.Core.DTOs
{
    // Kullanıcı tarafına Modle class'lar yerine dto class'lar döndürülür.
    // Best Practice yöntem budur.
    // AppUserDto class kullanıcı tarafı ile etkileşime geçecek class.

    // AppUserDto class
    public class AppUserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

    }
}
