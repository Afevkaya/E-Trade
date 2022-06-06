using AutoMapper;
using E_Trade.Core.DTOs;
using E_Trade.Core.Models;
using E_Trade.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Trade.API.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;

        public ProductsController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _service = productService;
        }

        // GET api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();
            var productsDto = _mapper.Map<List<ProductDto>>(products);
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
            Thread.Sleep(3000);
            return CreatActionResult<List<ProductDto>>(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
        }

        // GET api/products/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            /*
            var product = await _service.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return CreatActionResult<ProductDto>(CustomResponseDto<ProductDto>.Success(200, productDto));
            */
            Thread.Sleep(3000);
            return CreatActionResult<ProductWithCategoryDto>(await _service.GetProductWithCategory(id));
        }

        // GET api/products/ProductsWithCategory
        [HttpGet("[action]")]
        public async Task<IActionResult> ProductsWithCategory()
        {
            //return CreatActionResult<List<ProductsWithCategoryDto>>(CustomResponseDto<List<ProductsWithCategoryDto>>.Success(200, await _productService.GetProductsWithCategory()));
            return CreatActionResult<List<ProductsWithCategoryDto>>(await _service.GetProductsWithCategory());
        }

        // POST api/products
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            /*
            var product = _mapper.Map<Product>(productDto);
            var productDtos = _mapper.Map<ProductDto>(await _service.AddAsync(product));
            return CreatActionResult<ProductDto>(CustomResponseDto<ProductDto>.Success(201, productDtos));
            */

            return CreatActionResult(await _service.AddUpdateAsync(productDto));
        }

        // PUT api/produts
        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            var product = await _service.GetByIdAsync(productDto.Id);
            product = _mapper.Map<Product>(productDto);
            await _service.Update(product);
            return CreatActionResult<NoContentDto>(CustomResponseDto<NoContentDto>.Success(204));
        }

        // DELETE api/products/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.Remove(product);
            return CreatActionResult<NoContentDto>(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpGet]
        [Route("[action]/{searchText}")]
        public IActionResult Search(string searchText)
        {
            var products =  _service.Where(s => s.Name.Contains(searchText));
            var productsDto = _mapper.Map<List<ProductDto>>(products);
            return CreatActionResult<List<ProductDto>>(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
        }
    }
}
