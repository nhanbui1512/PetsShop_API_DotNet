using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Dtos.Product;
using petshop.Interfaces;
using petshop.Mappers;
using petshop.Models;
using PetsShop_API_DotNet.Dtos.Product;
using PetsShop_API_DotNet.Repository;

namespace petshop.Controllers
{
    [Route("/api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IProductRepository _productRepo;
        private readonly ImageRepository _imageRepository;

        public ProductController(AppDbContext productContext, IProductRepository productRepo)
        {
            this._dbContext = productContext;
            _productRepo = productRepo;

        }
        [HttpGet]
        public async Task<IActionResult> GetProducts(string? search, int page = 1, int limit = 5)
        {

            int maxLimit = 100;
            if (limit > maxLimit)
            {
                limit = maxLimit;
            }

            try
            {
                var producst = await _productRepo.GetAllAsync(search, page, limit);
                return Ok(producst);
            }
            catch (System.Exception)
            {
                return BadRequest("Internal Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO form)
        {

            var category = await _dbContext.Categories.FirstOrDefaultAsync(item => item.Id == form.CategoryId);
            if (category == null) return NotFound(new { message = "Not found category", status = 404 });

            var result = await _productRepo.AddProduct(form);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
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
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var product = await _productRepo.GetById(id);
            if (product == null) return NotFound(new { message = "Not found product" });
            return Ok(product);
        }
    }
}