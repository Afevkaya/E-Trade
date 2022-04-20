using AutoMapper;
using E_Trade.Core.DTOs;
using E_Trade.Core.Models;
using E_Trade.Core.Services;
using Microsoft.AspNetCore.Identity;

namespace E_Trade.Service.Services
{
    // User ile ilgili işlemlerin(metodların) bulunduğu class.
    // User ile ilgili db işlemleri identity içinde bulunan UserManager class içinde bulunur.
    // Bu yüzden repo katmanında bir kodlama yapılmaz.

    // UserService class.
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        // User ekleme metod. Add
        public async Task<CustomResponseDto<AppUserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            if (createUserDto is null)
            {
                throw new ArgumentNullException(nameof(createUserDto));
            }

            var appUser = new AppUser { Email = createUserDto.Email, UserName = createUserDto.UserName };
            var result = await _userManager.CreateAsync(appUser, createUserDto.Password);

            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<AppUserDto>.Fail(400, errors);
            }
            return CustomResponseDto<AppUserDto>.Success(200, _mapper.Map<AppUserDto>(appUser));

        }

        // User arama metod. Search
        public async Task<CustomResponseDto<AppUserDto>> GetUserByNameAsync(string userName)
        {
            if (userName == null)
            {
                throw new Exception(userName + " not null");
            }
            var appUser = await _userManager.FindByNameAsync(userName);
            if (appUser == null)
            {
                return CustomResponseDto<AppUserDto>.Fail(404, "User not Found");
            }

            return CustomResponseDto<AppUserDto>.Success(200, _mapper.Map<AppUserDto>(appUser));
        }
    }
}
