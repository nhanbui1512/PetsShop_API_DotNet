using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petshop.Dtos.Role
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string? RoleName { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}