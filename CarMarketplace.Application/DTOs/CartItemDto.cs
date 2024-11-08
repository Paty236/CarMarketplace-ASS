namespace CarMarketplace.Application.DTOs
{
    public class CartItemDto
    {
        public Guid Id { get; set; }
        public UserDto User { get; set; }
        public int Quantity { get; set; }
        public ProductDetailsDto Product { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
