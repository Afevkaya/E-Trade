namespace E_Trade.Core.DTOs
{
    // Entity class'ına ait kodlanılan Custom metodlar için Dto class'lar oluşturulabilir.
    // Category entity class için yazdığımız custom metod için oluşturduğumuz Dto class.

    // CategoryByIdWithProductsDto class
    public class CategoryByIdWithProductsDto : CategoryDto
    {
        public List<ProductDto> Products { get; set; }
    }
}
