
using System.ComponentModel.DataAnnotations;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Dtos.Image;
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
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsers(string? sort, string? search, [FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 100)] int perPage = 10)
        {
            GetUserDTO data = new GetUserDTO { Page = page, PerPage = perPage, Search = search, Sort = sort };
            var result = await _repository.GetAll(data);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {

            var user = await _repository.GetById(id);
            if (user == null) return NotFound(new { message = "Not found user", sttus = StatusCodes.Status404NotFound });
            return Ok(new { data = user, status = StatusCodes.Status200OK });
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
        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUser)
        {

            var context = HttpContext;
            var user = context.User.Claims;
            var userId = user.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (userId == null) return NotFound(new { message = "Not found user", status = StatusCodes.Status404NotFound });

            var result = await _repository.Update(updateUser, int.Parse(userId));
            if (result == null) return NotFound(new { message = "Not found user", status = StatusCodes.Status404NotFound });
            return Ok(new { data = result, status = StatusCodes.Status200OK });
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            bool result = await _repository.Remove(id);
            if (result == false) return NotFound(new { message = "Not found User" });
            return Ok(new { message = "Delete success" });
        }

        [HttpPatch]
        [Authorize]
        [Route("password")]
        public async Task<IActionResult> ChangePassword(PasswordDTO data)
        {
            var context = HttpContext;
            var user = context.User.Claims;
            var userId = user.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (userId == null) return NotFound(new { message = "Not found user", status = StatusCodes.Status404NotFound });

            var result = await _repository.ChangePassword(data, int.Parse(userId));
            if (result == null) return BadRequest(new { message = "Not found user", status = StatusCodes.Status404NotFound });
            if (result == false) return BadRequest(new { message = "Old password is wrong", status = StatusCodes.Status404NotFound });
            var newData = await _dbContext.Users.FindAsync(int.Parse(userId));

            return Ok(newData);
        }

        [HttpPatch]
        [Authorize]
        [Route("change-avatar")]
        public async Task<IActionResult> UpdateAvatar([FromForm] UpdateImageDTO data)
        {

            var context = HttpContext;
            var user = context.User.Claims;
            var userId = user.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (userId == null) return NotFound(new { message = "Not found user", status = StatusCodes.Status404NotFound });


            if (data?.Image?.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(data.Image.FileName)}";
                var filePath = Path.Combine("uploads", fileName);
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);

                byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await data.Image.CopyToAsync(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }

                await System.IO.File.WriteAllBytesAsync(fullPath, fileBytes);
                var result = await _repository.UpdateAvatar(filePath, int.Parse(userId));
                if (result == null) return NotFound(new { message = "Not found user", status = StatusCodes.Status404NotFound });
                result.AccessToken = null;
                result.RefreshToken = null;
                result.Password = null;
                return Ok(result);
            }
            return BadRequest(new { message = "Nout found image file" });
        }


    }
}