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
            var order = await _context.Orders.Include(or => or.OrderItems)
            .ThenInclude(op => op.Option)
            .FirstOrDefaultAsync(or => or.Id == id);
            if (order == null) return null;
            return order;
        }

        public async Task<PagedResult<Order>?> GetOrders(int page, int perPage, string sortBy, string search)
        {
            var total = await _context.Orders.CountAsync();
            var orders = _context.Orders.AsQueryable();


            #region Searching
            if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                orders = orders.Where(o => o.UserName.Contains(search)
                || o.Address.Contains(search) || o.PhoneNumber.Contains(search));
            #endregion
            orders = orders.Skip((page - 1) * perPage).Take(perPage);
            var result = await orders.ToListAsync();
            PagedResult<Order> paging = new PagedResult<Order>(result, total, page, perPage);
            return paging;
        }
    }
}