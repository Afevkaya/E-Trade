using AutoMapper;
using E_Trade.Core.DTOs;
using E_Trade.Core.Models;
using E_Trade.Core.Repositories;
using E_Trade.Core.Services;
using E_Trade.Core.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_Trade.Service.Services
{
    public class BasketService : IBasketService
    {
        // Parametreler
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BasketService(IBasketRepository basketRepository, IProductRepository productRepository, UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _productRepository = productRepository;
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // Ekleme Metodu
        public async Task<CustomResponseDto<ResponseBasketDto>> AddAsync(CreateBasketDto basketDto)
        {
            if (basketDto == null)
            {
                throw new ArgumentNullException(nameof(basketDto));
            }

            var baskets = await _basketRepository.GetAllAsync();
            Basket basket;

            // Sepet Kontrol
            if (baskets == null)
            {
                return CustomResponseDto<ResponseBasketDto>.Fail(500, "Server error");
            }

            var oldbasket = baskets.FirstOrDefault(x => x.ProductId == basketDto.ProductId);

            // dto -> entity
            if (oldbasket == null)
            {
                basket = new Basket();
                basket.ProductId = basketDto.ProductId;
                basket.AppUserId = basketDto.AppUserId;
                basket.ProductQuantity = basketDto.ProductQuantity;
                basket.Total = (basketDto.ProductQuantity) * (basketDto.ProductPrice);
            }
            else
            {
                basket = oldbasket;
                basket.ProductQuantity += basketDto.ProductQuantity;
                basket.Total = basket.Total + ((basketDto.ProductQuantity) * (basketDto.ProductPrice)); 
            }
            
            var product = await _productRepository.GetByIdAsync(basket.ProductId);
            var appUser = await _userManager.FindByIdAsync(basket.AppUserId);

            // Product Kontrol
            if (product == null)
            {
                return CustomResponseDto<ResponseBasketDto>.Fail(404, "Product Not Found");
            }

            // AppUser Kontrol
            if (appUser == null)
            {
                return CustomResponseDto<ResponseBasketDto>.Fail(404, "User Not Found");
            }

            // StockQuantity Kontrol
            if (product.StockQuantity < basket.ProductQuantity)
            {
                return CustomResponseDto<ResponseBasketDto>.Fail(404, "There is no product in stock or you have added more than the in stock product.");
            }

            product.StockQuantity -= basketDto.ProductQuantity;
            if (oldbasket != null)
            {
                _basketRepository.Update(basket);
            }
            else
            {
                await _basketRepository.AddAsync(basket);
            }
            _productRepository.Update(product);
            await _unitOfWork.CommitAsync();

            
            // entity -> dto
            var responseBasketDto = new ResponseBasketDto
            {
                ProductName = product.Name,
                AppUserName = appUser.UserName,
                ProductPrice = basket.Product.Price,
                ProductQuantity = basket.ProductQuantity,
                Total = (basket.Product.Price) * (basket.ProductQuantity)
            };

            return CustomResponseDto<ResponseBasketDto>.Success(201, responseBasketDto);
        }

        // Listeleme Metodu
        public async Task<CustomResponseDto<IEnumerable<ResponseBasketDto>>> GetAllAsync()
        {
            var baskets = await _basketRepository.GetAllAsync();

            // Kontrol
            if(baskets == null)
            {
                return CustomResponseDto<IEnumerable<ResponseBasketDto>>.Fail(404, "Basket not found");
            }
            var responseBasketDtos = new List<ResponseBasketDto>();
            
            // entity -> dto
            foreach (var basket in baskets)
            {
                var product = await _productRepository.GetByIdAsync(basket.ProductId);
                var appUser = await _userManager.FindByIdAsync(basket.AppUserId);
                responseBasketDtos.Add(new ResponseBasketDto
                {
                    ProductName = product.Name,
                    AppUserName = appUser.UserName,
                    ProductPrice = basket.Product.Price,
                    ProductQuantity = basket.ProductQuantity,
                    Total = (basket.Product.Price) * (basket.ProductQuantity)
                });
            }
            
            
            return CustomResponseDto<IEnumerable<ResponseBasketDto>>.Success(200, responseBasketDtos.AsEnumerable());
        }

        // Basket Id'ye göre Listeleme
        public async Task<CustomResponseDto<ResponseBasketDto>> GetByIdAsync(int id)
        {
            // Kontroller
            var basket = await _basketRepository.GetByIdAsync(id);
            if(basket == null)
            {
                return CustomResponseDto<ResponseBasketDto>.Fail(404, "Not Found");
            }

            var product = await _productRepository.GetByIdAsync(basket.ProductId);
            if(product == null)
            {
                return CustomResponseDto<ResponseBasketDto>.Fail(404, "Not Found");
            }

            var appUser = await _userManager.FindByIdAsync(basket.AppUserId);
            if (appUser == null)
            {
                return CustomResponseDto<ResponseBasketDto>.Fail(404, "Not Found");
            }

            // entity -> model
            var responseBasketDto = new ResponseBasketDto
            {
                ProductName = product.Name,
                AppUserName = appUser.UserName,
                ProductPrice = basket.Product.Price,
                ProductQuantity = basket.ProductQuantity,
                Total = (basket.Product.Price) * (basket.ProductQuantity)
            };
            return CustomResponseDto<ResponseBasketDto>.Success(200, responseBasketDto);
        }

        // Id'ye göre Silme
        public async Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id)
        {
            var basket = await _basketRepository.GetByIdAsync(id);

            // Kontrol
            if(basket == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, "Not Found");
            }
            _basketRepository.Remove(basket);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(200);
        }

        // AppUser Id'ye göre Listeleme
        public async Task<CustomResponseDto<IEnumerable<ResponseBasketDto>>> Where(Expression<Func<Basket, bool>> expression)
        {
            var baskets = await _basketRepository.Where(expression).ToListAsync();

            // Kontrol
            if(baskets == null)
            {
                return CustomResponseDto<IEnumerable<ResponseBasketDto>>.Fail(404, "NotFound");
            }

            var responseBasketDtos = new List<ResponseBasketDto>();

            // entity -> dto
            foreach(var basket in baskets)
            {
                var appUser = await _userManager.FindByIdAsync(basket.AppUserId);
                var product = await _productRepository.GetByIdAsync(basket.ProductId);
                responseBasketDtos.Add(new ResponseBasketDto
                {
                    ProductName = product.Name,
                    AppUserName = appUser.UserName,
                    ProductPrice = basket.Product.Price,
                    ProductQuantity = basket.ProductQuantity,
                    Total = (basket.Product.Price) * (basket.ProductQuantity)
                });
            }

            return CustomResponseDto<IEnumerable<ResponseBasketDto>>.Success(200, responseBasketDtos.AsEnumerable());

        }

        
    }
}
