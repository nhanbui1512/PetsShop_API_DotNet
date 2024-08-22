using System.ComponentModel.DataAnnotations;
using petshop.Validations;
namespace petshop.Dtos.Image
{
  public class UploadImageDTO
  {
    [Required]
    [ImageValidaton(new string[] { ".jpg", ".jpeg", ".png" })]
    public IFormFile? Image { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int ProductId { get; set; }

  }
}