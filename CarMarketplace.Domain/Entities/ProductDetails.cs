using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using CarMarketplace.Domain.Enums;

namespace CarMarketplace.Domain.Entities
{
    public class ProductDetails
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ProductCategory Category { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Details { get; set; } = null!;
        public byte[]? Image { get; set; } = null!;
        public decimal TotalPrice => Math.Round(Quantity * UnitPrice, 2);
    }
}
