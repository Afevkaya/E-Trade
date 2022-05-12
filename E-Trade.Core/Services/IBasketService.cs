using E_Trade.Core.DTOs;
using E_Trade.Core.Models;
using System.Linq.Expressions;

namespace E_Trade.Core.Services
{
    public interface IBasketService
    {
        Task<CustomResponseDto<IEnumerable<ResponseBasketDto>>> GetAllAsync();
        Task<CustomResponseDto<IEnumerable<ResponseBasketDto>>> Where(Expression<Func<Basket, bool>> expression);
        Task<CustomResponseDto<ResponseBasketDto>> GetByIdAsync(int id);
        Task<CustomResponseDto<ResponseBasketDto>> AddAsync(CreateBasketDto basketDto);
        Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id);

    }
}
