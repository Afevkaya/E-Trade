using E_Trade.Core.DTOs;
using E_Trade.Core.Models;
using E_Trade.Core.Services;
using E_Trade.Service.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace E_Trade.Service.Services
{
    // Token üretecek class.
    // Token ile ilgili bütün metodlar bu class içinde bulunur.
    // Access Token ve Refresh Token için gerekli feature'ları üreten metodların bulunduğu class.

    // TokenService class
    public class TokenService : ITokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly CustomTokenOption _tokenOptions;

        public TokenService(UserManager<AppUser> userManager, IOptions<CustomTokenOption> options)
        {
            _userManager = userManager;
            _tokenOptions = options.Value;
        }

        // RefreshToken üreten metod
        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }

        // Claim oluşturma metod
        private IEnumerable<Claim> GetClaims(AppUser appUser, List<string> audinces, string role)
        {
            var userList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,appUser.Id),
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim(ClaimTypes.Name,appUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,role),
                new Claim("userId",appUser.Id),
                new Claim("roles", role),
            };
            userList.AddRange(audinces.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            return userList;
        }

        // Token üreten metod
        public async Task<TokenDto> CreateToken(AppUser appUser)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration);

            var secretKey = SignService.GetSymmetricSecurityKey(_tokenOptions.SecurityKey);

            var role = (await _userManager.GetRolesAsync(appUser)).FirstOrDefault();

            SigningCredentials signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
            
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaims(appUser,_tokenOptions.Audience, role),
                signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration,
            };
            return tokenDto;
        }
    }
}
