using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using petshop.Data;
using petshop.Dtos.Category;
using petshop.Dtos.User;
using petshop.Interfaces;
using PetsShop_API_DotNet.Models;

namespace petshop.Repository
{
    public class AuthRepository(AppDbContext context, IConfiguration configuration) : IAuthRepository
    {
        private readonly AppDbContext _context = context;
        private readonly IConfiguration _configuration = configuration;
        public async Task<UserDTO> CheckLogin(LoginDTO data)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == data.Email);
            if (user == null) return null;

            PasswordService passwordService = new PasswordService();
            string hashedPassword = passwordService.HashPassword(user.Password);
            try
            {
                passwordService.VerifyPassword(user.Password, data.Password);
            }
            catch (System.Exception)
            {
                return null;
            }

            var cailms = new[]{
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Email", user.Email.ToString()),
                new Claim("UserId",user.Id.ToString()),
            };

            string secretKey = Environment.GetEnvironmentVariable("SECRET_KEY");
            string refreshTokenKey = Environment.GetEnvironmentVariable("REFRESH_TOKEN_KEY");

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var accessToken = new JwtSecurityToken(issuer, audience, cailms, expires: DateTime.UtcNow.AddDays(7), signingCredentials: signIn);

            var refreshKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(refreshTokenKey));
            var signInRefreshToken = new SigningCredentials(refreshKey, SecurityAlgorithms.HmacSha256);
            var refreshToken = new JwtSecurityToken(issuer, audience, cailms, expires: DateTime.UtcNow.AddDays(30), signingCredentials: signInRefreshToken);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(accessToken);
            string refreshTokenValue = new JwtSecurityTokenHandler().WriteToken(refreshToken);



            RefreshToken newRefreshToken = new RefreshToken
            {
                TokenValue = refreshTokenValue,
                ExpireTime = DateTime.UtcNow.AddDays(30),
                User = user,

            };

            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();
            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Email = user.Email,
                Avatar = user.Avatar,
                FullName = user.FullName,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                CreatedAtStr = user.CreatedAtStr,
                UpdatedAtStr = user.UpdatedAtStr,
                AccessToken = tokenValue,
                RefreshToken = refreshTokenValue
            };
        }
    }
}