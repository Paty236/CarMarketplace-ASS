using System.Security.Claims;

namespace CarMarketplace.Application.DTOs
{
    public class ClaimsDto
    {
        public string UserId { get; set; } = "userId";
        public string Role { get; set; } = "role";

        public ClaimsDto(string userId, string role)
        {
            UserId = userId;
            Role = role;
        }
        public ClaimsDto() { }

        public ClaimsDto(IEnumerable<Claim> claims)
        {
            UserId = claims.Single(c => c.Type == "userId").Value;
            Role = claims.Single(c => c.Type == "role").Value;
        }

        public static string Id = "userId";
        public static string UserRole = "role";
    }
}
