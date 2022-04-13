using E_Trade.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Trade.Core.Services
{
    public interface IUserService
    {
        Task<CustomResponseDto<AppUserDto>> CreateUser(CreateUserDto createUserDto);
        Task<CustomResponseDto<AppUserDto>> GetUserByName(string userName);
    }
}
