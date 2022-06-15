using E_Trade.Core.DTOs;
using FluentValidation;

namespace E_Trade.Service.Validations
{
    // CategoryDto class için validasyon işlemlerini yapıldığı class.
    // Validasyon işlemlerinin kullanılabilmesi için Fluent Validasion pakedinin olması gerekli.
    // Validasyon class olabilmesi için AbstractValidator<> generic abstarc class'ını inherit etmesi gerekli.
    
    // FluentValidation pakedi ile gelen hazır validatorlar server tarafına gitmez. Client tarafında hata mesajı gösterilir.(Bazıları hariç)
    // Kendi yazdığımız custom validatorlarda hata mesajı görünmesi için sunucu tarafına gidip gelmesi gereklidir.

    // CategoryDtoValidator class.
    // Custom Validotor Class
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        // Validasyon kuralları constructor içinde tanımlanır.
        public CategoryDtoValidator()
        {
            // Validasyon kuralları RuleFor metodu ile konulur.
            
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} alanı null olamaz").NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
        }
    }
}
