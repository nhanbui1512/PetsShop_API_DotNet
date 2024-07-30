using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using petshop.Data;
using petshop.Dtos.Category;
using petshop.Dtos.Product;
using petshop.Interfaces;

namespace PetsShop_API_DotNet.Controllers
{
    [Route("/api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        // [GET] /api/categories
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _repository.GetAll();
            return Ok(categories);
        }

        // [POST] /api/categories 
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _repository.Add(data);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById([FromRoute] int id, [FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 100)] int perPage = 10)
        {
            var category = await _repository.GetById(id, page, perPage);
            if (category == null) return NotFound(new { message = "Not found category" });
            return Ok(category);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryDTO data)
        {
            var result = await _repository.Update(data, id);
            if (result == null) return NotFound(new { message = "Not found category" });
            return Ok(new { status = "Success", newData = result });
        }
    }
}