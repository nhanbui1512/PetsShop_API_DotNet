
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PetsShop_API_DotNet.Interfaces;

namespace petshop.Controllers
{

    [Route("/api/images")]
    [ApiController]
    public class ImageController(IImageRepository imageRepository) : ControllerBase
    {
        private readonly IImageRepository _imageRepository = imageRepository;

        [HttpGet]
        public async Task<IActionResult> GetImagesByProductId(int product_id)
        {
            if (product_id <= 0) return BadRequest(new { message = "product_id not validation" });
            var images = await _imageRepository.GetImages(product_id);
            return Ok(images);
        }

        [HttpDelete]
        [Route("{image_id}")]
        public async Task<IActionResult> DeleteImage([FromRoute, Range(1, int.MaxValue)] int image_id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            bool result = await _imageRepository.Remove(image_id);
            if (!result) return BadRequest(new { message = "Not found image", status = 404 });

            return Ok(new { message = "Delete image successfully", status = 200 });
        }

    }
}