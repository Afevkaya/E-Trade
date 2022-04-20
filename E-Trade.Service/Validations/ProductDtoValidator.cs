﻿using E_Trade.Core.DTOs;
using FluentValidation;

namespace E_Trade.Service.Validations
{
    // ProductDto class için validasyon işlemlerini yapıldığı class.
    // Validasyon işlemlerinin kullanılabilmesi için Fluent Validasion pakedinin olması gerekli.
    // Validasyon class olabilmesi için AbstractValidator<> generic abstarc class'ını inherit etmesi gerekli.

    // ProductDtoValidator class.
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} alanı null olamaz").NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
            RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} alanı minimum 1 olmalıdır");
            RuleFor(x => x.Description).NotNull().WithMessage("{PropertyName} alanı null olamaz").NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
            RuleFor(x => x.ImageUrl).NotNull().WithMessage("{PropertyName} alanı null olamaz").NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
            RuleFor(x => x.Quantity).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} alanı minimum 1 olmalıdır");
            RuleFor(x => x.CategoryId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} alanı minimum 1 olmalıdır");
        }
    }
}
