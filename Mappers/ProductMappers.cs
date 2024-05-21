using petshop.Dtos.Product;
using petshop.Models;

namespace petshop.Mappers
{
    public static class ProductMappers
    {
        public static Product ToProductObject(this CreateProductDTO dto)
        {

            List<Option> Options = new List<Option>();

            foreach (var item in dto.CreateOptionDTOs)
            {
                Options.Add(item.ToOptionObject());
            }
            return new Product
            {
                ProductName = dto.ProductName,
                Options = Options,
                Category = new Category { Id = dto.CategoryId }
            };
        }
    }
}