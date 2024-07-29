
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Dtos.Product;
using petshop.Interfaces;
using PetsShop_API_DotNet.Repository;
using Swashbuckle.AspNetCore.Annotations;

namespace petshop.Controllers
{
    [Route("/api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IProductRepository _productRepo;

        public ProductController(AppDbContext productContext, IProductRepository productRepo)
        {
            _dbContext = productContext;
            _productRepo = productRepo;

        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get list product")]
        public async Task<IActionResult> GetProducts(string? sortBy, string? search, int page = 1, int limit = 5)
        {

            int maxLimit = 100;
            if (limit > maxLimit)
            {
                limit = maxLimit;
            }

            try
            {
                var producst = await _productRepo.GetAllAsync(search, page, limit, sortBy);
                return Ok(producst);
            }
            catch (Exception)
            {
                return BadRequest("Internal Error");
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a product")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO form)
        {

            var category = await _dbContext.Categories.FirstOrDefaultAsync(item => item.Id == form.CategoryId);
            if (category == null) return NotFound(new { message = "Not found category", status = 404 });

            var result = await _productRepo.AddProduct(form);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Delete a product")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {

            var product = await _dbContext.Products.FirstOrDefaultAsync(item => item.Id == id);
            if (product == null) return NotFound();
            else
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
                return new JsonResult(new { message = "Delete product successfully" }) { StatusCode = 200 };

            }

        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Get product by id")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var product = await _productRepo.GetById(id);
            if (product == null) return NotFound(new { message = "Not found product" });
            return Ok(product);
        }
    }
}