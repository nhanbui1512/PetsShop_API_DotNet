using petshop.Dtos.Role;
using PetsShop_API_DotNet.Dtos.Role;
using PetsShop_API_DotNet.Models;

namespace PetsShop_API_DotNet.Mappers
{
    public static class RoleMappers
    {
        public static Role ToRoleObject(this CreateRoleDTO form)
        {
            return new Role
            {
                RoleName = form.RoleName,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            };
        }

        public static RoleDTO ToRoleDTO(this Role role)
        {
            return new RoleDTO
            {
                Id = role.Id,
                RoleName = role.RoleName,
                CreateAt = role.CreateAt,
                UpdateAt = role.UpdateAt,
            };

        }
    }
}