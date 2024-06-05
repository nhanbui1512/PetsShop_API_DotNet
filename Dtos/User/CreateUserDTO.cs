using System.ComponentModel.DataAnnotations;

namespace petshop.Dtos.User
{
    public class CreateUserDTO
    {

        [Required]
        [MinLength(5, ErrorMessage = "First name must be characters")]
        [MaxLength(280, ErrorMessage = "First name cannot be over 280 characters")]
        public string FirstName { get; set; } = String.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Last name must be characters")]
        [MaxLength(280, ErrorMessage = "Last name cannot be over 280 characters")]
        public string LastName { get; set; } = String.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Email must be 15 characters")]
        [MaxLength(280, ErrorMessage = "Email cannot be over 280 characters")]
        public string Email { get; set; } = String.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Password must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Password cannot be over 280 characters")]
        public string Password { get; set; } = String.Empty;

        [Required]
        public bool Gender { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}