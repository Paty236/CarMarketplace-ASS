using CarMarketplace.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarMarketplace.Application.DTOs
{
    public class CarDto
    {
        public Guid Id { get; set; }
        public CarMake Make { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public CarCondition Condition { get; set; }
        public string? Color { get; set; }
        public int Mileage { get; set; } // Kilometraj
        public Guid SellerId { get; set; }
        public string? SellerName { get; set; }
        public List<byte[]>? Images { get; set; } // Poze ale mașinii
        public DateTime CreatedAt { get; set; }
    }
}
