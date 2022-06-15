using E_Trade.Core.DTOs;
using FluentValidation;

namespace E_Trade.Service.Validations
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x=>x.Email).NotNull().WithMessage("{PropertyName} null olamaz").NotEmpty().WithMessage("{PropertyName} boş olamaz").EmailAddress().WithMessage("{PropertyName} uygun formatta olmalıdır.");
            RuleFor(x => x.Password).NotNull().WithMessage("{PropertyName} null olamaz").NotEmpty().WithMessage("{PropertyName} boş olamaz");
        }
    }
}
