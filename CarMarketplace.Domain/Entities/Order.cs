using MongoDB.Bson;
using CarMarketplace.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace CarMarketplace.Domain.Entities
{
    public class Order
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        public User Buyer { get; set; } // Cumpărătorul
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public Status Status { get; set; } // Statusul tranzacției 
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
