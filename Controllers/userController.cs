
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using petshop.Data;

namespace petshop.Controllers
{
    [Route("/api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AppDbContext _dbContext;

        public UserController(AppDbContext productDbContext)
        {
            _dbContext = productDbContext;
        }
        [HttpGet]
        public IActionResult getUsers()
        {
            var result = _dbContext.Users.ToList().Select(user => user.toUserDTO());
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult getUserById([FromRoute] int id){
            var user = _dbContext.Users.Find(id);
            if(user != null){
                return Ok(user.toUserDTO());
            }
            else{
                return NotFound();
            }
        }
    }
}