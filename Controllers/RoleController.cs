using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Dtos.Role;
using PetsShop_API_DotNet.Dtos.Role;
using PetsShop_API_DotNet.Mappers;
using PetsShop_API_DotNet.Models;

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
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var result = await _dbContext.Roles.Select(role => new { role.Id, role.RoleName, role.CreateAt, role.UpdateAt }).ToListAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "An unexpected error occurred", detail = e.Message });
            }

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDTO formData)
        {
            try
            {
                string? name = formData.RoleName;
                if (name == null) return new JsonResult(new { RoleName = "Not validation" }) { StatusCode = 403 };

                var isExist = await _dbContext.Roles.FirstOrDefaultAsync(role => role.RoleName == name);
                if (isExist != null) return new JsonResult(new { message = "NameRole already exist" }) { StatusCode = 409 };
                var NewRole = formData.ToRoleObject();
                _dbContext.Add(NewRole);
                await _dbContext.SaveChangesAsync();
                return Ok(NewRole);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred", detail = ex.Message });
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDTO form)
        {

            if (form.RoleId == null) return new BadRequestObjectResult(new { RoleId = "Not Validation", status = 409 }) { StatusCode = 409 };
            if (form.RoleName == null || form.RoleName.Trim() == "") return new BadRequestObjectResult(new { roleName = "Not Validation", status = 409 }) { StatusCode = 409 };

            var role = await _dbContext.Roles.FirstOrDefaultAsync(role => role.Id == form.RoleId);
            if (role == null) return NotFound();

            role.RoleName = form.RoleName;
            role.UpdateAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                id = role.Id,
                roleName = role.RoleName,
                createAt = role.CreateAt,
                updateAt = role.UpdateAt
            });

        }
    }
}