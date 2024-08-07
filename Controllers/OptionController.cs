
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using petshop.Dtos.Option;
using petshop.Interfaces;

namespace petshop.Controllers
{
    [ApiController]
    [Route("/api/options")]

    public class OptionController(IOptionRepository repository) : ControllerBase
    {

        private readonly IOptionRepository _repository = repository;

        [HttpGet]
        [Route("{product_id:int}")]
        public async Task<IActionResult> getOption([FromRoute] int product_id)
        {

            var data = await _repository.GetByProductId(product_id);
            if (data == null) return NotFound(new { message = "Not found product" });
            return Ok(data);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOption([FromBody] CreateOptionDTO data, [FromQuery] int? product_id)
        {
            if (!product_id.HasValue)
            {
                return BadRequest(new { message = "Product Id is required", status = 400 });
            }

            var newOption = await _repository.Add(data, product_id);
            if (newOption == null) return NotFound(new { message = "Not found product" });
            return Ok(new { newData = newOption, status = 200 });
        }

        [Authorize]
        [HttpDelete]
        [Route("{option_id:int}")]
        public async Task<IActionResult> DeleteOption([FromRoute] int option_id)
        {
            Boolean result = await _repository.Remove(option_id);
            if (!result) return NotFound(new { message = "Not found option" });
            return Ok(new { message = "Delete Option successfully" });
        }

        [Authorize]
        [HttpPatch]
        [Route("{option_id:int}")]
        public async Task<IActionResult> UpdateOption([FromBody] UpdateOptionDTO data, [FromRoute] int option_id)
        {
            var result = await _repository.Update(data, option_id);
            if (result == null) return NotFound(new { message = "Not found option" });
            return Ok(result);
        }


    }
}