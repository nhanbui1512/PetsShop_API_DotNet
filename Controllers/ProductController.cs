using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Dtos.Product;
using petshop.Mappers;
using petshop.Models;

namespace petshop.Controllers
{
    [Route("/api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public ProductController(AppDbContext productContext)
        {
            this._dbContext = productContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _dbContext.Products.ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO form)
        {

            var category = await _dbContext.Categories.FirstOrDefaultAsync(item => item.Id == form.CategoryId);
            if (category == null) return NotFound(new { message = "Not found category", status = 404 });


            var options = new List<Option>();
            foreach (var item in form.CreateOptionDTOs)
            {
                options.Add(item.ToOptionObject());
            }
            var NewProduct = new Product
            {
                ProductName = form.ProductName,
                Category = category,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Options = options
            };

            await _dbContext.Products.AddAsync(NewProduct);
            await _dbContext.SaveChangesAsync();
            return Ok(NewProduct);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {

            var product = await _dbContext.Products.FirstOrDefaultAsync(item => item.Id == id);
            if (product == null) return NotFound(new { messagge = "not found" });
            else
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
                return NoContent();

            }

        }
    }
}