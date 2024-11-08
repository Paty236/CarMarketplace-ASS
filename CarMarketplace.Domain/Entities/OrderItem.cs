using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CarMarketplace.Domain.Entities
{
    public class OrderItem
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid CarId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
