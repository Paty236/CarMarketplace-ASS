using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CarMarketplace.Domain.Entities
{
    public class CartItem
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        public User User { get; set; }
        public int Quantity { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid ProductId { get; set; }
        public ProductDetails Product { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
