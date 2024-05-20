using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsShop_API_DotNet.Dtos.User
{
    public class UpdateUserDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool Gender { get; set; }

    }
}