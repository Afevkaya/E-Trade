using E_Trade.Core.DTOs;
using FluentValidation;

namespace E_Trade.Service.Validations
{
    public class CreateBasketDtoValidator : AbstractValidator<CreateBasketDto>
    {
        public CreateBasketDtoValidator()
        {
            RuleFor(x => x.AppUserId).NotNull().WithMessage("{PropertyName} alanı null olamaz").NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
            RuleFor(x => x.ProductId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} alanı minimum 1 olmalıdır");
            RuleFor(x => x.ProductQuantity).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} alanı minimum 1 olmalıdır");
            RuleFor(x => x.ProductPrice).InclusiveBetween(1, decimal.MaxValue).WithMessage("{PropertyName} alanı minimum 1 olmalıdır");
        }
    }
}
