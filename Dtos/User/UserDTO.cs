
using petshop.Dtos.Role;

namespace petshop.Dtos.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Avatar { get; set; }
        public bool Gender { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public RoleDTO? Role { get; set; }
        public string? FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CreatedAtStr { get; set; }
        public string? UpdatedAtStr { get; set; }
    }
}