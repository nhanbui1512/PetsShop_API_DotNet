
using Microsoft.AspNetCore.Mvc;
using petshop.Interfaces;

namespace petshop.Controllers
{
    [ApiController]
    [Route("/api/options")]

    public class OptionController : ControllerBase
    {

        private readonly IOptionRepository _repository;

        public OptionController(IOptionRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [Route("{product_id:int}")]
        public async Task<IActionResult> getOption([FromRoute] int product_id)
        {

            var data = await _repository.GetByProductId(product_id);
            if (data == null) return NotFound(new { message = "Not found product" });
            return Ok(data);
        }

        [HttpDelete]
        [Route("option_id:int")]
        public async Task<IActionResult> DeleteOption([FromRoute] int option_id){
            
            return Ok();
        }
    }
}