namespace E_Trade.Core.DTOs
{
    // Product ile ilgili bir işlem gerçekleştirirken kullanılacak Dto class.

    // ProductDto class.
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
    }
}
