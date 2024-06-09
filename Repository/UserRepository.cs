using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Dtos.Category;
using petshop.Dtos.Product;
using petshop.Dtos.Role;
using petshop.Dtos.User;
using petshop.Interfaces;
using petshop.Models;
using PetsShop_API_DotNet.Dtos.User;

namespace petshop.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<UserDTO> Add(CreateUserDTO data)
        {

            throw new NotImplementedException();
        }

        public async Task<List<UserDTO>> GetAll()
        {
            var users = await _context.Users.Include(u => u.Role).Select(u => new UserDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Avatar = u.Avatar,
                Gender = u.Gender,
                Role = new RoleDTO { Id = u.Role.Id, RoleName = u.Role.RoleName, CreateAt = u.Role.CreateAt, UpdateAt = u.Role.UpdateAt }

            }).ToListAsync();

            return users;
        }

        public Task<UserDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Remove(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;

        }

        public Task<UserDTO> Update(UpdateUserDTO data, int id)
        {
            throw new NotImplementedException();
        }
    }
}