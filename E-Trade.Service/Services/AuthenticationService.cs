﻿using AutoMapper;
using E_Trade.Core.DTOs;
using E_Trade.Core.Models;
using E_Trade.Core.Repositories;
using E_Trade.Core.Services;
using E_Trade.Core.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Trade.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<UserRefreshToken> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(ITokenService tokenService, UserManager<AppUser> userManager, IMapper mapper,
            IGenericRepository<UserRefreshToken> genericRepository, IUnitOfWork unitOfWork)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _mapper = mapper;
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }

        // User'a token dönecek metod.
        public async Task<CustomResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }

            // user --> AppUser 
            var appUser = await _userManager.FindByEmailAsync(loginDto.Email);
            if(appUser == null)
            {
                return CustomResponseDto<TokenDto>.Fail(400, "Email or password wrong");
            }

            if(!await _userManager.CheckPasswordAsync(appUser, loginDto.Password))
            {
                return CustomResponseDto<TokenDto>.Fail(400, "Email or password wrong!");
            }

            var tokenDto = _tokenService.CreateToken(appUser);
            if (tokenDto == null)
            {
                return CustomResponseDto<TokenDto>.Fail(500, "An error occurred");
            }

            // Db'de user'a ait bir kullanıcıya ait bir refresh token olup olmadığını kontrol eden if else bloğu
            // User'a ait önceden bir refresh token yoksa ekleme yapıyor.
            // userRefreshToken --> UserRefreshToken
            var userRefreshToken = await _genericRepository.Where(x => x.UserId == appUser.Id).SingleOrDefaultAsync();
            if(userRefreshToken == null)
            {
                await _genericRepository.AddAsync(new UserRefreshToken { UserId = appUser.Id, Code = tokenDto.RefreshToken, Expiration = tokenDto.RefreshTokenExpiration });
            }

            // Varsa güncelleme
            else
            {
                userRefreshToken.Code = tokenDto.RefreshToken;
                userRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;
            }
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<TokenDto>.Success(200, tokenDto);
        }

        // Token üretme metodu refreshtoken'a göre
        public async Task<CustomResponseDto<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            if (refreshToken == null)
            {
                throw new ArgumentNullException(nameof(refreshToken));
            }

            // Token'ın db kontrolü
            // UserRefreshToken veya null dönüyor
            var existRefreshToken = await _genericRepository.Where(x=>x.Code == refreshToken).SingleOrDefaultAsync();
            if(existRefreshToken == null)
            {
                return CustomResponseDto<TokenDto>.Fail(404, "Refresh Token Not Found");
            }

            // userApp kontrolü
            // Token üretmek için userApp ihtiyaç.
            var appUser = await _userManager.FindByIdAsync(existRefreshToken.UserId);
            if(appUser == null)
            {
                return CustomResponseDto<TokenDto>.Fail(404, "User Id Not Found");
            }

            // Token üretme
            var tokenDto = _tokenService.CreateToken(appUser);
            if (tokenDto == null)
            {
                return CustomResponseDto<TokenDto>.Fail(500, "An error occurred");
            }

            // db deki token'ı üretilen token ile güncelleme
            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<TokenDto>.Success(200, tokenDto);

            
        }

        // RefreshToken silme metodu.
        public async Task<CustomResponseDto<NoContentDto>> RevokeRefreshToken(TokenDto tokenDto)
        {
            if (tokenDto == null)
            {
                throw new ArgumentNullException(nameof(tokenDto));
            }

            // db'de var olup olmadığı kontrolü
            var refreshtoken = await _genericRepository.Where(x=>x.Code == tokenDto.RefreshToken).SingleOrDefaultAsync();
            if(refreshtoken == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, "Refresh Token Not Found");
            }

            // db'den silme
            _genericRepository.Remove(refreshtoken);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(200);
        }
    }
}
