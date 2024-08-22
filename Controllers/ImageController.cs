
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
        public async Task<IActionResult> GetImagesByProductId([FromQuery, Range(1, int.MaxValue)] int product_id)
        {
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
        public async Task<IActionResult> UploadImage([FromForm] UploadImageDTO data)
        {
            if (data?.Image?.Length > 0)
            {
                // Tạo tên file duy nhất bằng GUID
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(data.Image.FileName)}";

                // Đường dẫn lưu file
                var filePath = Path.Combine("uploads", fileName);
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);

                // Chuyển file thành binary
                byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await data.Image.CopyToAsync(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }

                // Lưu file dưới dạng binary
                await System.IO.File.WriteAllBytesAsync(fullPath, fileBytes);
                var imageLinks = new List<string>(){
                    filePath
                };

                var result = await _imageRepository.AddImages(imageLinks, data.ProductId);
                if (result == null) return NotFound(new { message = "Not found product" });

                return Ok(result);
            }
            return BadRequest(new { message = "Not found image file" });
        }
        [HttpPatch]
        [Route("{image_id}")]
        public async Task<IActionResult> UpdateImage([FromForm] UpdateImageDTO data, [FromRoute, Range(1, int.MaxValue)] int image_id)
        {

            if (data?.Image?.Length > 0)
            {
                // Tạo tên file duy nhất bằng GUID
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(data.Image.FileName)}";

                // Đường dẫn lưu file
                var filePath = Path.Combine("uploads", fileName);
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);

                // Chuyển file thành binary
                byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await data.Image.CopyToAsync(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }

                // Lưu file dưới dạng binary
                await System.IO.File.WriteAllBytesAsync(fullPath, fileBytes);

                var result = await _imageRepository.UpdateImage(filePath, image_id);
                if (result == null) return NotFound(new { message = "Not found image" });

                return Ok(result);
            }
            return BadRequest(new { message = "Not found image file" });

        }

    }
}