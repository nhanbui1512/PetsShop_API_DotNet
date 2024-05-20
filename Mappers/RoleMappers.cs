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
    }
}