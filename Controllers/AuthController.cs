using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using petshop.Data;
using petshop.Dtos.Category;
using dotenv.net;
using petshop.Interfaces;



namespace petshop.Controllers
{
    [Route("/api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IAuthRepository _repository;

        public AuthController(AppDbContext userContext, IAuthRepository repository, IConfiguration configuration)
        {
            _dbContext = userContext;
            _repository = repository;
            this._configuration = configuration;

            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
        }
        [HttpPost]
        [Route("/api/auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == data.Email && u.Password == data.Password);
            var user = await _repository.CheckLogin(data);
            if (user == null) return NotFound(new { message = "Not found user" });

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
            var token = new JwtSecurityToken(issuer, audience, cailms, expires: DateTime.UtcNow.AddDays(7), signingCredentials: signIn);

            var refreshKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(refreshTokenKey));
            var signInRefreshToken = new SigningCredentials(refreshKey, SecurityAlgorithms.HmacSha256);
            var refreshToken = new JwtSecurityToken(issuer, audience, cailms, expires: DateTime.UtcNow.AddDays(30), signingCredentials: signInRefreshToken);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            string refreshTokenValue = new JwtSecurityTokenHandler().WriteToken(refreshToken);


            user.RefreshToken = refreshTokenValue;
            user.AccessToken = tokenValue;
            await _dbContext.SaveChangesAsync();

            return Ok(new { accessToken = tokenValue, refreshToken = refreshTokenValue, data = user });
        }

    }
}