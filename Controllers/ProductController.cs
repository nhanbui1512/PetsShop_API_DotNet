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

            var Category = await _dbContext.Categories.FirstOrDefaultAsync(item => item.Id == form.CategoryId);
            if (Category == null) return NotFound(new { message = "Not found category", status = 404 });

            var NewProduct = new Product
            {
                ProductName = form.ProductName,
            };
            NewProduct.Category = Category;

            _dbContext.Products.Add(NewProduct);
            await _dbContext.SaveChangesAsync();

            return Ok(NewProduct);
        }
    }
}