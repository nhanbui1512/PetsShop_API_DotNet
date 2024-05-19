using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using PetsShop_API_DotNet.Dtos.Role;
using PetsShop_API_DotNet.Mappers;

namespace PetsShop_API_DotNet.Controllers
{
    [Route("/api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly AppDbContext _dbContext;

        public RoleController(AppDbContext context)
        {
            _dbContext = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDTO formData)
        {

            try
            {
                string? name = formData.RoleName;
                if (name == null) return new JsonResult(new { RoleName = "Not validation" }) { StatusCode = 403 };

                var isExist = await _dbContext.Roles.FirstOrDefaultAsync(role => role.RoleName == name);
                if (isExist != null) return new JsonResult(new { message = "NameRole already exist" }) { StatusCode = 409 };
                var NewRole = formData.ToUserObject();
                await _dbContext.AddAsync(NewRole);
                return Ok(NewRole);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred", detail = ex.Message });
            }

        }
    }
}