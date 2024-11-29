using System.ComponentModel.DataAnnotations;

namespace PetsShop_API_DotNet.Dtos.User
{
    public class UpdateUserDTO
    {

        [MaxLength(280, ErrorMessage = "First name cannot be over 280 characters")]
        public string FirstName { get; set; }

        [MaxLength(280, ErrorMessage = "Last name cannot be over 280 characters")]
        public string LastName { get; set; }
        public bool? Gender { get; set; }

    }
}