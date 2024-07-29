using System.ComponentModel.DataAnnotations;
using petshop.Validations;

namespace petshop.Dtos.User
{
    public class PasswordDTO
    {
        [Required]
        [MinLength(8, ErrorMessage = "Old Password must be at least 8 characters long.")]
        public string? OldPassword { get; set; }

        [Required]
        [MaxLength(30)]
        [PasswordComplexity]
        public string? NewPassword { get; set; }
    }
}