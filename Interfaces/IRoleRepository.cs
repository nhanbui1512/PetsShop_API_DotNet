

using petshop.Dtos.Category;
using petshop.Dtos.User;
using PetsShop_API_DotNet.Models;

namespace petshop.Interfaces
{
  public interface IRoleRepository
  {
    Task<Role> CreateRole(string roleName);
    Task<Role?> Update(int roleId, string nameRole);
    Task<bool> Delete(int roleId);
    Task<List<Role>> GetRoles();
    Task<Role?> GetById(int roleId);

  }
}