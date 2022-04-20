using E_Trade.Core.DTOs;

namespace E_Trade.Core.Services
{
    // User ile ilgili işlemlerin(metodların) bulunduğu service interface.
    // User ile ilgili repo işlemleri yazılmaz.
    // Identity framework içinde bulunan UserManager class'ı gerekli tüm işlemleri(metodları) içinde bulundurur.

    // IUserService
    public interface IUserService
    {
        Task<CustomResponseDto<AppUserDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<CustomResponseDto<AppUserDto>> GetUserByNameAsync(string userName);
    }
}
