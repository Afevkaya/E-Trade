using AutoMapper;
using E_Trade.Core.DTOs;
using E_Trade.Core.Models;
using E_Trade.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Trade.API.Controllers
{
    public class ProductsController : CustomBaseController
    {
        private readonly IService<Product> _service;
        private readonly IMapper _mapper;

        
        public ProductsController(IMapper mapper, IService<Product> service)
        {
            _mapper = mapper;
            _service = service;
        }

        // GET api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();
            var productsDto = _mapper.Map<List<ProductDto>>(products);
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
            return CreatActionResult<List<ProductDto>>(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
        }

        // GET api/products/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return CreatActionResult<ProductDto>(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        // POST api/products
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var productDtos = _mapper.Map<ProductDto>(await _service.AddAsync(product));
            return CreatActionResult<ProductDto>(CustomResponseDto<ProductDto>.Success(201, productDtos));
        }

        // PUT api/produts
        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _service.Update(product);
            return CreatActionResult<NoContentDto>(CustomResponseDto<NoContentDto>.Success(204));
        }

        // DELETE api/products/id
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.Remove(product);
            return CreatActionResult<NoContentDto>(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
