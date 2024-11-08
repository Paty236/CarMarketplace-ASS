using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using CarMarketplace.Domain.Enums;

namespace CarMarketplace.Domain.Entities
{
    public class Car
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public CarMake Make { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        [BsonRepresentation(BsonType.String)]
        public CarCondition Condition { get; set; }
        public string? Color { get; set; }
        public int Mileage { get; set; } // Kilometraj
        public virtual User Seller { get; set; }
        public List<byte[]> Images { get; set; } // Poze ale mașinii
        public DateTime CreatedAt { get; set; }
    }
}
