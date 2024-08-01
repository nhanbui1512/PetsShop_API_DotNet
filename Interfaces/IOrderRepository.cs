
using petshop.Models;

namespace petshop.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> Create(Order data);
        Task<Order?> GetById(int id);
        Task<List<Order>?> GetOrders(int page, int perPage, string sortBy);
    }
}