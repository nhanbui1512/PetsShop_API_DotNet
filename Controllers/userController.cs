
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Dtos.User;

namespace petshop.Controllers
{
    [Route("/api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AppDbContext _dbContext;

        public UserController(AppDbContext userContext)
        {
            _dbContext = userContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _dbContext.Users.Select(user => user.toUserDTO()).ToListAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user != null)
            {
                return Ok(user.toUserDTO());
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserDOT formData)
        {
            var isExist = _dbContext.Users.SingleOrDefault(user => user.Email == formData.Email);
            if (isExist != null)
            {
                return Conflict(new { message = "Email already exists", status = 409 });
            }
            var userObject = formData.ToFormCreateUser();
            _dbContext.Users.Add(userObject);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetUserById), new { id = userObject.Id }, userObject.toUserDTO());
        }
    }
}