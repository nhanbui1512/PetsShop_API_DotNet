using api.Mappers;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Dtos.Category;
using petshop.Dtos.User;
using petshop.Interfaces;

namespace petshop.Repository
{
    public class AuthRepository(AppDbContext context) : IAuthRepository
    {
        private readonly AppDbContext _context = context;
        public async Task<UserDTO?> CheckLogin(LoginDTO data)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Password == data.Password && u.Email == data.Email);
            if (user == null) return null;
            return user.ToUserDTO();
        }
    }
}