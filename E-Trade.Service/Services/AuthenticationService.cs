﻿using AutoMapper;
using E_Trade.Core.DTOs;
using E_Trade.Core.Models;
using E_Trade.Core.Repositories;
using E_Trade.Core.Services;
using E_Trade.Core.UnitOfWorks;
using E_Trade.Service.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Trade.Service.Services
{
    // Authentication işlemlerini(metodlarını) kodladığımız class.
    // IAuthenticationService interface'ini implement eder.
    // TokenService class bu service içerisinde kullanılır.

    // AuthenticationService class.
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

        // User'a token dönecek asenkron metod.
        public async Task<CustomResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }

            // appUser --> AppUser 
            var appUser = await _userManager.FindByEmailAsync(loginDto.Email);
            if(appUser == null)
            {
                throw new NotFoundException($"{loginDto.Email} or {loginDto.Password} wrong");
            }

            if(!await _userManager.CheckPasswordAsync(appUser, loginDto.Password))
            {
                throw new NotFoundException($"{loginDto.Email} or {loginDto.Password} wrong");
            }

            var tokenDto = await _tokenService.CreateToken(appUser);
            if (tokenDto == null)
            {
                throw new Exception("An error occurred");
            }

            // Db'de user'a ait bir refresh token olup olmadığını kontrol eden if else bloğu
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
                throw new NotFoundException($"{existRefreshToken} not found");
            }

            // userApp kontrolü
            // Token üretmek için userApp ihtiyaç.
            var appUser = await _userManager.FindByIdAsync(existRefreshToken.UserId);
            if(appUser == null)
            {
                throw new NotFoundException($"{appUser} not found");
            }

            // Token üretme
            var tokenDto = await _tokenService.CreateToken(appUser);
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
        public async Task<CustomResponseDto<NoContentDto>> RevokeRefreshToken(string refreshToken)
        {
            if (refreshToken == null)
            {
                throw new ArgumentNullException(nameof(refreshToken));
            }

            // db'de var olup olmadığı kontrolü
            var existRefreshToken = await _genericRepository.Where(x=>x.Code == refreshToken).SingleOrDefaultAsync();
            if(existRefreshToken == null)
            {
                throw new NotFoundException($"{existRefreshToken} not found");
            }

            // db'den silme
            _genericRepository.Remove(existRefreshToken);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(200);
        }
    }
}
