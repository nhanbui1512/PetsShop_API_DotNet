using System.ComponentModel.DataAnnotations;
using petshop.Validations;
namespace petshop.Dtos.Image
{
  public class UpdateImageDTO
  {
    [Required]
    [ImageValidaton(new string[] { ".jpg", ".jpeg", ".png" })]
    public IFormFile? Image { get; set; }
  }
}