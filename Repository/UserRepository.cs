using api.Mappers;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Dtos.Role;
using petshop.Dtos.User;
using petshop.Interfaces;
using petshop.Models;
using PetsShop_API_DotNet.Dtos.User;
using PetsShop_API_DotNet.Mappers;

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

        public async Task<PagedResult<UserDTO>> GetAll(GetUserDTO data)
        {
            var count = await _context.Users.CountAsync();
            var allUsers = await _context.Users.Include(u => u.Role).Select(u => new UserDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Avatar = u.Avatar,
                Gender = u.Gender,
                FullName = u.FullName,
                Role = new RoleDTO { Id = u.Role.Id, RoleName = u.Role.RoleName, CreateAt = u.Role.CreateAt, UpdateAt = u.Role.UpdateAt }

            }).ToListAsync();


            if (!string.IsNullOrEmpty(data.Search))
            {
                allUsers = allUsers.Where(u => u.Email.ToLower().Contains(data.Search.ToLower()) || u.FullName.ToLower().Contains(data.Search.ToLower())).ToList();
            }

            var result = allUsers.Skip((data.Page - 1) * data.PerPage).Take(data.PerPage).ToList();
            PagedResult<UserDTO> paging = new PagedResult<UserDTO>(result, count, data.Page, data.PerPage);
            return paging;
        }

        public async Task<UserDTO> GetById(int id)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return null;
            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName,
                Email = user.Email,
                Avatar = user.Avatar,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                CreatedAtStr = user.CreatedAtStr,
                UpdatedAtStr = user.UpdatedAtStr,
                Role = user.Role.ToRoleDTO(),
            };
        }

        public async Task<bool> Remove(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<UserDTO> Update(UpdateUserDTO data, int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;

            if (!string.IsNullOrEmpty(data.FirstName) && !string.IsNullOrWhiteSpace(data.FirstName)) user.FirstName = data.FirstName;
            if (!string.IsNullOrEmpty(data.LastName) && !string.IsNullOrWhiteSpace(data.LastName)) user.LastName = data.LastName;
            if (data.Gender.HasValue) user.Gender = data.Gender.Value;
            user.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return user.ToUserDTO();
        }

        public async Task<bool?> ChangePassword(PasswordDTO data, int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;
            if (user.Password != data.OldPassword) return false;
            user.Password = data.NewPassword;
            user.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<User> SaveChange(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> UpdateAvatar(string filePath, int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return null;
            user.Avatar = filePath;
            user.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return user;
        }
    }
}