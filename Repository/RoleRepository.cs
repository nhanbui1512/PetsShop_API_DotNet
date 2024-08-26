using petshop.Data;
using petshop.Interfaces;
using PetsShop_API_DotNet.Models;

namespace petshop.Repository
{
  public class RoleRepository : IRoleRepository
  {
    private readonly AppDbContext _context;
    public RoleRepository(AppDbContext context)
    {
      this._context = context;
    }

    public Task<Role> CreateRole(string roleName)
    {
      throw new NotImplementedException();
    }

    public Task<bool> Delete(int roleId)
    {
      throw new NotImplementedException();
    }

    public Task<Role?> GetById(int roleId)
    {
      throw new NotImplementedException();
    }

    public Task<List<Role>> GetRoles()
    {
      throw new NotImplementedException();
    }

    public Task<Role> Update(int roleId, string nameRole)
    {
      throw new NotImplementedException();
    }
  }
}