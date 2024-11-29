using Microsoft.EntityFrameworkCore;
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

    public async Task<Role> CreateRole(string roleName)
    {
      Role newRole = new Role()
      {
        RoleName = roleName
      };
      _context.Roles.Add(newRole);
      await _context.SaveChangesAsync();
      return newRole;
    }

    public async Task<bool> Delete(int roleId)
    {
      var role = await _context.Roles.FirstOrDefaultAsync(role => role.Id == roleId);
      if (role == null) return false;
      _context.Roles.Remove(role);
      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<Role> GetById(int roleId)
    {
      var role = await _context.Roles.FirstOrDefaultAsync(role => role.Id == roleId);
      if (role == null) return null;
      return role;
    }

    public async Task<List<Role>> GetRoles()
    {
      var roles = await _context.Roles.ToListAsync();
      return roles;
    }

    public async Task<Role> Update(int roleId, string roleName)
    {
      var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
      if (role == null) return null;
      role.RoleName = roleName;
      await _context.SaveChangesAsync();
      return role;
    }
  }
}