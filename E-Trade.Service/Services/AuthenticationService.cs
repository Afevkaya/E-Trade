using AutoMapper;
using E_Trade.Core.DTOs;
using E_Trade.Core.Models;
using E_Trade.Core.Repositories;
using E_Trade.Core.Services;
using E_Trade.Core.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<CustomResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<NoContentDto>> RevokeRefreshToken(TokenDto tokenDto)
        {
            throw new NotImplementedException();
        }
    }
}
