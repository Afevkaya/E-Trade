using AutoMapper;
using E_Trade.Core.DTOs;
using E_Trade.Core.Models;
using E_Trade.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Trade.API.Controllers
{
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> All()
        {
            return CreatActionResult(await _basketService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreatActionResult(await _basketService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CreateBasketDto createBasketDto)
        {
            return CreatActionResult(await _basketService.AddAsync(createBasketDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreatActionResult(await _basketService.RemoveAsync(id));
        }

        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetByAppUserId(string userId)
        {
            return CreatActionResult(await _basketService.Where(x => x.AppUserId == userId));
        }


    }
}
