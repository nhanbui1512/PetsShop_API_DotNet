using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using petshop.Data;
using petshop.Dtos.Category;
using petshop.Interfaces;


namespace petshop.Controllers
{
    [Route("/api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext userContext, IConfiguration configuration)
        {
            _dbContext = userContext;
            this._configuration = configuration;
        }
        [HttpPost]
        [Route("/api/auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO data)
        {

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == data.Email && u.Password == data.Password);
            if (user == null) return NotFound(new { message = "Not found user" });

            var cailms = new[]{
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Email", user.Email.ToString()),
                new Claim("UserId",user.Id.ToString()),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var token = new JwtSecurityToken(issuer, audience, cailms, expires: DateTime.UtcNow.AddMinutes(60), signingCredentials: signIn);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { accessToken = tokenValue, data = user.ToUserDTO() });
        }

    }
}