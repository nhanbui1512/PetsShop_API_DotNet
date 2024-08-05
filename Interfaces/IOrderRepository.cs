
using petshop.Dtos.Orders;
using petshop.Models;

namespace petshop.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> Create(Order data);
        Task<OrderDTO?> GetById(int id);
        Task<PagedResult<Order>?> GetOrders(int page, int perPage, string sortBy, string search);
        Task<List<Order>?> PrepareOrders(int[] OrderIds);
        Task<bool?> Delete(int orderId);
        Task<List<Order>?> ConfirmOrders(int[] orderIds);
    }
}