
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
        public RoleDTO? Role { get; set; }
    }
}