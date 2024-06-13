
using System.ComponentModel.DataAnnotations;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Dtos.User;
using petshop.Interfaces;

using PetsShop_API_DotNet.Dtos.User;

namespace petshop.Controllers
{
    [Route("/api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AppDbContext _dbContext;
        private readonly IUserRepository _repository;

        public UserController(AppDbContext userContext, IUserRepository repository)
        {
            _dbContext = userContext;
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers(int page, int perPage, string? sort, string? search)
        {
            GetUserDTO data = new GetUserDTO { Page = page, PerPage = perPage, Search = search, Sort = sort };
            if (data.PerPage == 0) data.PerPage = 5;
            var result = await _repository.GetAll(data);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {

            var user = await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
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

            if (!ModelState.IsValid) return BadRequest(ModelState);

            string email = formData.Email;
            var isExist = await _dbContext.Users.SingleOrDefaultAsync(user => user.Email == email);
            if (isExist != null)
            {
                return new JsonResult(new { message = "Email already exists", status = 409 }) { StatusCode = 409 };
            }
            else
            {
                var userObject = formData.ToFormCreateUser();
                var role = await _dbContext.Roles.FindAsync(userObject.RoleId);
                if (role == null) return NotFound(new { role = "Not found role" });
                userObject.Role = role;
                _dbContext.Users.Add(userObject);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUserById), new { id = userObject.Id }, userObject);
            }

        }

        [HttpPut]
        [Route("{id:int}")]
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

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            bool result = await _repository.Remove(id);
            if (result == false) return NotFound(new { message = "Not found User" });
            return Ok(new { message = "Delete success" });
        }
    }
}