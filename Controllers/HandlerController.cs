using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using petshop.Data;
using petshop.Models;

namespace PetsShop_API_DotNet.Controllers
{
    [ApiController]
    [Route("/api/handle")]
    public class HandlerController : ControllerBase

    {
        private readonly AppDbContext _context;

        public HandlerController(AppDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {


            return Ok("Created");

        }

        public void AddProducts()
        {

        }

        public void AddCategories()
        {
            string filePath = "Collections/formated/categories.formated.json"; // Đường dẫn tới tệp JSON của bạn
            string json = System.IO.File.ReadAllText(filePath);
            JArray jsonArray = JArray.Parse(json);

            foreach (JObject item in jsonArray)
            {
                string name = item["name"]?.ToString();
                string description = item["description"]?.ToString();
                DateTime createdAt = item["createdAt"].ToObject<DateTime>();
                DateTime updatedAt = item["updatedAt"].ToObject<DateTime>();

                var newCategory = new Category
                {
                    CategoryName = name,
                    Description = description,
                    CreateAt = createdAt,
                    UpdateAt = updatedAt
                };

                _context.Categories.Add(newCategory);
                _context.SaveChangesAsync();
                Console.WriteLine(name);
            }
        }
    }
}