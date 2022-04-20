namespace E_Trade.Core.DTOs
{
    // Entity class'ına ait kodlanılan Custom metodlar için Dto class'lar oluşturulabilir.
    // Product entity class için yazdığımız custom metod için oluşturduğumuz Dto class.

    // ProductWithCategory class
    public class ProductsWithCategoryDto : ProductDto
    {
        public CategoryDto Category { get; set; }
    }
}
