using E_Trade.Service.Configurations;
using E_Trade.Core.DTOs;
using E_Trade.Core.Models;
using E_Trade.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_Trade.Service.Services
{
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
        private IEnumerable<Claim> GetClaims(AppUser appUser, List<string> audinces)
        {
            var userList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,appUser.Id),
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim(ClaimTypes.Name,appUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            userList.AddRange(audinces.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return userList;
        }

        // Toek üreten metod
        public TokenDto CreateToken(AppUser appUser)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration);

            var secretKey = SignService.GetSymmetricSecurityKey(_tokenOptions.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
            
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaims(appUser,_tokenOptions.Audience),
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
