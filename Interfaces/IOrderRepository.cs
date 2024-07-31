
using petshop.Models;

namespace petshop.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> Create(Order data);
    }
}