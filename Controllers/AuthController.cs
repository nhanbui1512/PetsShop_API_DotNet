using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserRepository _repository;
        public AuthController(AppDbContext userContext, IUserRepository repository)
        {
            _dbContext = userContext;
            _repository = repository;
        }
        [HttpPost]
        [Route("/api/auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO data)
        {
            return Ok();
        }

    }
}