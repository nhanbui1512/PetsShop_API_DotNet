using Microsoft.AspNetCore.Mvc;
using petshop.Data;
using petshop.Interfaces;

namespace PetsShop_API_DotNet.Controllers
{
    [Route("/api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly AppDbContext _dbContext;
        private readonly ICategoryRepository _repository;

        public CategoryController(AppDbContext context, ICategoryRepository repository)
        {
            _dbContext = context;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _repository.GetAll();
            return Ok(categories);
        }
    }
}