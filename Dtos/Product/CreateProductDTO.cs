using petshop.Dtos.Option;
using petshop.Models;

namespace petshop.Dtos.Product
{
    public class CreateProductDTO
    {

        public string? ProductName { get; set; }
        public string? Image { get; set; }
        public int CategoryId { get; set; }
        public List<CreateOptionDTO> CreateOptionDTOs { get; set; } = new List<CreateOptionDTO>();
    }
}