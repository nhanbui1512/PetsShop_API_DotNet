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

    }
}