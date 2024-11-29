using System.ComponentModel.DataAnnotations;

namespace petshop.Dtos.Category
{
  public class RefreshTokenDTO
  {
    [Required]
    public string RefreshToken { get; set; }

  }
}