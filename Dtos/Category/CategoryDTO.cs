
using PetsShop_API_DotNet.Dtos.Product;

namespace petshop.Dtos.Category
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<GetProductDTO> Products { get; set; }
    }
}