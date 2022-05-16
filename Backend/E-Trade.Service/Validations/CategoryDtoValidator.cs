using E_Trade.Core.DTOs;
using FluentValidation;

namespace E_Trade.Service.Validations
{
    // CategoryDto class için validasyon işlemlerini yapıldığı class.
    // Validasyon işlemlerinin kullanılabilmesi için Fluent Validasion pakedinin olması gerekli.
    // Validasyon class olabilmesi için AbstractValidator<> generic abstarc class'ını inherit etmesi gerekli.

    // CategoryDtoValidator class.
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} alanı null olamaz").NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
        }
    }
}
