namespace E_Trade.Core.DTOs
{
    // Kullanıcı tarafı login işlemi gerçekleştirirken kullanıcı tarafı ile etkileşime geçecek dto class.

    // LoginDto class
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
