
using petshop.Models;
using PetsShop_API_DotNet.Dtos.Product;

namespace petshop.Interfaces
{
    public interface IProductRepository
    {
        Task<PagedResult<GetProductDTO>> GetAllAsync(string search, int page, int limit);
        Task<GetProductDTO> GetById(int Id);
    }
}