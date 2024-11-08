using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CarMarketplace.Domain.Entities
{
    public class UserRole
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
