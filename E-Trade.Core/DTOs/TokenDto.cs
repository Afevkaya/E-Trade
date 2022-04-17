namespace E_Trade.Core.DTOs
{
    // User tarafına döndürülecek TokenDto class
    // Class içinde AccessToken, RefreshToken ve ömürleri bulunmakta.

    // TokenDto Dto class
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }

    }
}
