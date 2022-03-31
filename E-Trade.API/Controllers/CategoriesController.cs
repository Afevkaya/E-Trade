using AutoMapper;
using E_Trade.Core.DTOs;
using E_Trade.Core.Models;
using E_Trade.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Trade.API.Controllers
{
    public class CategoriesController : CustomBaseController
    {
        private readonly IService<Category> _service;
        private readonly IMapper _mapper;

        public CategoriesController(IMapper mapper, IService<Category> service)
        {
            _mapper = mapper;
            _service = service;
        }

        // GET api/categories
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var categories = await _service.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
            return CreatActionResult<List<CategoryDto>>(CustomResponseDto<List<CategoryDto>>.Success(200, categoriesDto));
        }

        // GET api/categories/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _service.GetByIdAsync(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return CreatActionResult<CategoryDto>(CustomResponseDto<CategoryDto>.Success(200, categoryDto));
        }

        // POST api/categories 
        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            var categoryDtos = _mapper.Map<CategoryDto>(await _service.AddAsync(category));
            return CreatActionResult<CategoryDto>(CustomResponseDto<CategoryDto>.Success(201, categoryDtos));
        }

        // PUT api/categories 
        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _service.Update(category);
            return CreatActionResult<NoContentDto>(CustomResponseDto<NoContentDto>.Success(204));
        }

        // DELETE api/categories/id
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _service.GetByIdAsync(id);
            await _service.Remove(category);
            return CreatActionResult<NoContentDto>(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
