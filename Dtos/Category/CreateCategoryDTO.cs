using System.ComponentModel.DataAnnotations;
using petshop.Dtos.Option;
using petshop.Models;

namespace petshop.Dtos.Product
{
  public class CreateCategoryDTO
  {

    [Required]
    [MinLength(5)]
    [MaxLength(150)]
    public string? CategoryName { get; set; }
    [Required]
    [MinLength(5)]
    [MaxLength(150)]
    public string? Description { get; set; }
  }
}