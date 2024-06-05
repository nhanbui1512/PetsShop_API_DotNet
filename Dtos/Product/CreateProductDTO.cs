using System.ComponentModel.DataAnnotations;
using petshop.Dtos.Option;
using petshop.Models;

namespace petshop.Dtos.Product
{
    public class CreateProductDTO
    {

        [Required]
        [MinLength(5)]
        [MaxLength(150)]
        public string? ProductName { get; set; }
        [Required]
        public string? Image { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public List<CreateOptionDTO> CreateOptionDTOs { get; set; } = new List<CreateOptionDTO>();
    }
}