using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CarMarketplace.Domain.Entities
{
    public class User
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public UserRole UserRole { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityCode { get; set; }
        public List<Car> CarsForSale { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
