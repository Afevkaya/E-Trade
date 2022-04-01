using AutoMapper;
using E_Trade.Core.DTOs;
using E_Trade.Core.Models;
using E_Trade.Core.Repositories;
using E_Trade.Core.Services;
using E_Trade.Core.UnitOfWorks;

namespace E_Trade.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IGenericRepository<Category> repository, ICategoryRepository categoryRepository, IMapper mapper) : base(unitOfWork, repository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        /*
        public async Task<CategoryByIdWithProductsDto> GetSingleCategoryByIdProducts(int id)
        {
            var categoryWithProducts = await _categoryRepository.GetSingleCategoryByIdProducts(id);
            var categoryWithProductsDto = _mapper.Map<CategoryByIdWithProductsDto>(categoryWithProducts);
            return categoryWithProductsDto;
        }
        */

        public async Task<CustomResponseDto<CategoryByIdWithProductsDto>> GetSingleCategoryByIdProducts(int categoryId)
        {
            var category = await _categoryRepository.GetSingleCategoryByIdProducts(categoryId);
            var categoryWithProductsDto = _mapper.Map<CategoryByIdWithProductsDto>(category);
            return CustomResponseDto<CategoryByIdWithProductsDto>.Success(200, categoryWithProductsDto);
        }
    }
}
