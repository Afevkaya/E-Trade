namespace E_Trade.Core.DTOs
{
    // Kullanıcı tarafına döndürülecek Dto class.
    // Class içinde AccessToken, RefreshToken ve ömürleri bulunmakta.
    // Bu bilgiler kullanıcının tarafında saklanacak.

    // TokenDto Dto class
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }

    }
}
