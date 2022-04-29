namespace E_Trade.Core.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
