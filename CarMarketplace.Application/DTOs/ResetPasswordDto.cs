using System.ComponentModel.DataAnnotations;

namespace CarMarketplace.Application.DTOs
{
    public class ResetPasswordDto
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Email must not be empty")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Code must not be empty")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Password must not be empty")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password must not be empty")]
        public string ConfirmPassword { get; set; }
    }
}
