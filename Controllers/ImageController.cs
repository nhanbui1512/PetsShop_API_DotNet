
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using petshop.Dtos.Image;
using PetsShop_API_DotNet.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace petshop.Controllers
{

    [Route("/api/images")]
    [ApiController]
    public class ImageController(IImageRepository imageRepository) : ControllerBase
    {
        private readonly IImageRepository _imageRepository = imageRepository;

        [SwaggerOperation(Summary = "Get images of product")]
        [HttpGet]
        public async Task<IActionResult> GetImagesByProductId(int product_id)
        {
            if (product_id <= 0) return BadRequest(new { message = "product_id not validation" });
            var images = await _imageRepository.GetImages(product_id);
            return Ok(new { data = images });
        }

        [SwaggerOperation(Summary = "Delete image of product")]
        [HttpDelete]
        [Route("{image_id}")]
        public async Task<IActionResult> DeleteImage([FromRoute, Range(1, int.MaxValue)] int image_id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            bool result = await _imageRepository.Remove(image_id);
            if (!result) return BadRequest(new { message = "Not found image", status = 404 });

            return Ok(new { message = "Delete image successfully", status = 200 });
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageDTO data)
        {
            if (data.Image.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", data.Image.FileName);
                using (var stream = System.IO.File.Create(path))
                {
                    await data.Image.CopyToAsync(stream);
                }

            }
            return Ok();
        }

    }
}