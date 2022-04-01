using E_Trade.Core.DTOs;
using FluentValidation;

namespace E_Trade.Service.Validations
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} alanı null olamaz").NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
        }
    }
}
