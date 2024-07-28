
using System.ComponentModel.DataAnnotations;

namespace petshop.Dtos.Option
{
    public class CreateOptionDTO
    {

        [Required]
        [MinLength(5, ErrorMessage = "Name must be characters")]
        [MaxLength(280, ErrorMessage = "Name cannot be over 280 characters")]
        public string? Name { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        [Range(typeof(decimal), "1.0", "79228162514264337593543950335", ErrorMessage = "Quantity must be at least 1.")]
        public decimal Price { get; set; }

    }
}