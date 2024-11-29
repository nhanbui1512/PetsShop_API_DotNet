
using petshop.Dtos.Product;
using petshop.Models;
using PetsShop_API_DotNet.Dtos.Product;

namespace petshop.Interfaces
{
    public interface IProductRepository
    {
        Task<PagedResult<GetProductDTO>> GetAllAsync(string search, int page, int limit, string sortBy);
        Task<GetProductDTO> GetById(int Id);
        Task<Product> AddProduct(CreateProductDTO data);
        Task<int> CountProductsOfCategory(int categoryId);
    }
}