namespace CarMarketplace.Application.DTOs
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public CarDto Car { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
