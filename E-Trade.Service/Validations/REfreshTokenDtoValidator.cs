using E_Trade.Core.DTOs;
using FluentValidation;

namespace E_Trade.Service.Validations
{
    public class RefreshTokenDtoValidator : AbstractValidator<RefreshTokenDto>
    {
        public RefreshTokenDtoValidator()
        {
            RuleFor(x=>x.Token).NotNull().WithMessage("{PropertyName} null olamaz").NotEmpty().WithMessage("{PropertyName} boş olamaz");
        }
    }
}
