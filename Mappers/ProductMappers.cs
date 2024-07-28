using petshop.Dtos.Product;
using petshop.Models;
using PetsShop_API_DotNet.Dtos.Product;

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

        public static GetProductDTO ToProductDTO(this Product product)
        {

            List<string> images = new List<string>();
            foreach (var item in product.Images)
            {
                images.Add(item.FileURL);
            }

            return new GetProductDTO
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Options = product.Options,
                Category = product.Category,
                Images = images,
                CreateAt = product.CreateAt,
                UpdateAt = product.UpdateAt,
                Description = product.Description,
                DOM = product.DOM
            };
        }
    }
}