namespace E_Trade.Core.DTOs
{
    // Bir user oluşturulurken kullanıcı tarafı ile etkileşime geçecek Dto class.

    // CreateUserDto class.
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


    }
}
