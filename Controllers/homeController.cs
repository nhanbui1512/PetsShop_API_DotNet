
using Microsoft.AspNetCore.Mvc;
using petshop.Data;

namespace petshop.Controllers
{
    [Route("/api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext productDbContext)
        {
            _dbContext = productDbContext;
        }
        [HttpGet]
        public IActionResult getUsers()
        {
            var result = _dbContext.Users;
            return Ok(result);
        }
    }
}