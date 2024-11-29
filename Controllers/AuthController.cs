using Microsoft.AspNetCore.Mvc;
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
            var user = await _repository.CheckLogin(data);
            if (user == null) return NotFound(new { message = "Not found user" });

            return Ok(new { data = user });
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDTO data)
        {
            try
            {
                var result = await _repository.RefreshAccessToken(data.RefreshToken);
                return Ok();
            }
            catch (System.Exception)
            {
                return Ok();
            }

        }

    }
}