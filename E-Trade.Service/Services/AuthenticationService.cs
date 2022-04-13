using E_Trade.Core.DTOs;
using E_Trade.Core.Models;
using E_Trade.Core.Repositories;
using E_Trade.Core.Services;
using E_Trade.Core.UnitOfWorks;
using Microsoft.AspNetCore.Identity;

namespace E_Trade.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenRepo;

        public AuthenticationService(UserManager<AppUser> userManager, ITokenService tokenService, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> genericRepository)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _userRefreshTokenRepo = genericRepository;
        }



        public Task<CustomResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<TokenDto>> CreateTokenAsyncByRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<NoContentDto>> RevokeRefreshtoken(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
