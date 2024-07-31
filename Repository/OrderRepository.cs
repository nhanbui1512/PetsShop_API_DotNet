using Microsoft.EntityFrameworkCore;
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

        public async Task<Order?> GetById(int id)
        {
            var order = await _context.Orders.Include(or => or.OrderItems).ThenInclude(op => op.Option).FirstOrDefaultAsync(or => or.Id == id);
            if (order == null) return null;
            return order;
        }
    }
}