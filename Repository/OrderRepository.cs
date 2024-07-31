using petshop.Data;
using petshop.Interfaces;
using petshop.Models;

namespace petshop.Repository
{
    public class OrderRepository(AppDbContext context) : IOrderRepository
    {
        private readonly AppDbContext _context = context;
        public async Task<Order?> Create(Order data)
        {
            _context.Orders.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}