
using Microsoft.AspNetCore.Mvc;

namespace petshop
{
    [Route("/api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public IActionResult home()
        {
            return Ok("Hello world");
        }

        [HttpPost]
        public IActionResult GetProduct(){
            return Ok();
        }
    }
}