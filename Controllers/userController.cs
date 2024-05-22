
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Dtos.User;
using petshop.Migrations;
using PetsShop_API_DotNet.Dtos.User;

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
            var result = await _dbContext.Users.Select(user => user.ToUserDTO()).ToListAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            // fix
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO formData)
        {
            string email = formData.Email;
            var isExist = await _dbContext.Users.SingleOrDefaultAsync(user => user.Email == email);
            if (isExist != null)
            {
                return new JsonResult(new { message = "Email already exists", status = 409 }) { StatusCode = 409 };
            }
            else
            {
                var userObject = formData.ToFormCreateUser();
                _dbContext.Users.Add(userObject);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUserById), new { id = userObject.Id }, userObject);
            }

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserDTO updateUser)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
            if (user == null) return NotFound();

            user.FirstName = updateUser.FirstName;
            user.LastName = updateUser.LastName;
            user.Gender = updateUser.Gender;
            await _dbContext.SaveChangesAsync();
            return new JsonResult(new
            {
                userId = user.Id,
                email = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName,
                gender = user.Gender,
            })
            { StatusCode = 200 };

        }
    }
}