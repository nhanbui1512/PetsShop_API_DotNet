
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using petshop.Data;
using petshop.Dtos.User;

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
        public IActionResult GetUsers()
        {
            var result = _dbContext.Users.Select(user => new
            {
                user.Id,
                user.Email,
                user.UserName,
                user.Gender
            }).ToList();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetUserById([FromRoute] int id)
        {
            var user = _dbContext.Users.Find(id);
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
            var userObject = formData.ToFormCreateUser();
            _dbContext.Users.Add(userObject);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetUserById), new { id = userObject.Id }, userObject.toUserDTO());
        }
    }
}