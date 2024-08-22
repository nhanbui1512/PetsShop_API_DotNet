using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using petshop.Data;
using petshop.Dtos.Category;
using petshop.Dtos.Product;
using petshop.Interfaces;
using PetsShop_API_DotNet.Dtos.Product;

namespace PetsShop_API_DotNet.Controllers
{
    [Route("/api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryRepository _categoryRepository;

        private readonly IProductRepository _productRepository;

        public CategoryController(ICategoryRepository repository, IProductRepository productRepository)
        {
            _categoryRepository = repository;
            _productRepository = productRepository;

        }

        // [GET] /api/categories
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetAll();
            return Ok(categories);
        }

        // [POST] /api/categories 
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _categoryRepository.Add(data);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById([FromRoute] int id, [FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 100)] int perPage = 10)
        {
            var category = await _categoryRepository.GetById(id, page, perPage);
            if (category == null) return NotFound(new { message = "Not found category", status = StatusCodes.Status404NotFound });

            var countProduct = await _productRepository.CountProductsOfCategory(id);
            PagedResult<GetProductDTO> paging = new PagedResult<GetProductDTO>(category.Products, countProduct, page, perPage);

            return Ok(new
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                Description = category.Description,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt,
                products = paging,
            });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryDTO data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _categoryRepository.Update(data, id);
            if (result == null) return NotFound(new { message = "Not found category", status = StatusCodes.Status404NotFound });

            return Ok(new { status = "Success", newData = result });
        }
        [HttpDelete]
        [Route("{id_category}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id_category)
        {
            _categoryRepository.Remove(id_category);
            return Ok();
        }
    }
}