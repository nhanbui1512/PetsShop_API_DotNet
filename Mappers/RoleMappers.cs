using PetsShop_API_DotNet.Dtos.Role;
using PetsShop_API_DotNet.Models;

namespace PetsShop_API_DotNet.Mappers
{
    public static class RoleMappers
    {
        public static Role ToUserObject(this CreateRoleDTO form)
        {
            return new Role
            {
                RoleName = form.RoleName,
                CreateAt = new DateTime(),
                UpdateAt = new DateTime(),
            };
        }
    }
}