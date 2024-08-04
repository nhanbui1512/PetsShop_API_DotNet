
using petshop.Models;

namespace petshop.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> Create(Order data);
        Task<Order?> GetById(int id);
        Task<PagedResult<Order>?> GetOrders(int page, int perPage, string sortBy, string search);
        Task<List<Order>?> PrepareOrders(int[] OrderIds);
        Task<bool?> Delete(int orderId);
    }
}