using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Dtos.Role;
using petshop.Interfaces;
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
        private readonly IRoleRepository _roleRepository;

        public RoleController(AppDbContext context, IRoleRepository roleRepository)
        {
            _dbContext = context;
            _roleRepository = roleRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var result = await _roleRepository.GetRoles();
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
                string name = formData.RoleName;
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
            if (form?.RoleId == null) return new BadRequestObjectResult(new { RoleId = "Not Validation", status = 409 }) { StatusCode = 409 };
            if (form.RoleName == null || form.RoleName.Trim() == "") return new BadRequestObjectResult(new { roleName = "Not Validation", status = 409 }) { StatusCode = 409 };

            var role = await _roleRepository.Update(form.RoleId, form.RoleName);
            if (role == null) return NotFound(new { message = "Not found role", status = 404 });
            return Ok(role);
        }
    }
}