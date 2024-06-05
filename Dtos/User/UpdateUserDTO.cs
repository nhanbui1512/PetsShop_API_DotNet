using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetsShop_API_DotNet.Dtos.User
{
    public class UpdateUserDTO
    {
        [Required]
        [MinLength(5, ErrorMessage = "First name must be characters")]
        [MaxLength(280, ErrorMessage = "First name cannot be over 280 characters")]
        public string? FirstName { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "First name must be characters")]
        [MaxLength(280, ErrorMessage = "First name cannot be over 280 characters")]
        public string? LastName { get; set; }

        [Required]
        public bool Gender { get; set; }

    }
}