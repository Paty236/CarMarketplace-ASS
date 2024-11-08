using System.ComponentModel.DataAnnotations;

namespace CarMarketplace.Application.DTOs
{
    public class RegisterDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "First Name must not be empty")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name must not be empty")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email must not be empty")]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid UserRoleId { get; set; }
        public string? Role { get; set; }
        [Required(ErrorMessage = "Password must not be empty")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password must not be empty")]
        public string ConfirmPassword { get; set; }
        public List<CarDto>? CarsForSale { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
