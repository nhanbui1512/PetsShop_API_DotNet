
using petshop.Models;

namespace petshop.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(string search, int page, int limit);

    }
}