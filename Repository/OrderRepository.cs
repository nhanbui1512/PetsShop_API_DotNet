using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Dtos.Orders;
using petshop.Interfaces;
using petshop.Mappers;
using petshop.Migrations;
using petshop.Models;
using PetsShop_API_DotNet.Mappers;

namespace petshop.Repository
{
    public class OrderRepository(AppDbContext context) : IOrderRepository
    {
        private readonly AppDbContext _context = context;
        public async Task<Order?> Create(Order data)
        {
            var orderItems = new List<OrderItem>();

            #region decrease quantity 
            var optionIds = data.OrderItems.Select(o => o.OptionId).ToArray();
            // find options of orderItems
            var options = await _context.Options.Where(o => optionIds.Contains(o.Id)).ToListAsync();

            foreach (var orderItem in data.OrderItems)
            {
                var option = options.Find(o => o.Id == orderItem.OptionId);
                if (option != null)
                {
                    if (orderItem.Quantity > option.Quantity) continue;
                    option.Quantity -= orderItem.Quantity;
                    orderItems.Add(orderItem);
                }
            }
            #endregion

            if (orderItems.Count == 0) return null;
            data.OrderItems = orderItems;
            _context.Orders.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool?> Delete(int orderId)
        {
            var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null) return null;

            var optionIds = order.OrderItems.Select(o => o.OptionId).ToArray();
            var options = await _context.Options.Where(o => optionIds.Contains(o.Id)).ToListAsync();

            foreach (var item in options)
            {
                var orderItem = order.OrderItems.Find(o => item.Id == o.OptionId);
                if (orderItem != null)
                {
                    item.Quantity += orderItem.Quantity;
                }
            }

            try
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<OrderDTO?> GetById(int id)
        {
            var order = _context.Orders.AsQueryable();

            order = order.Include(or => or.OrderItems)
                    .ThenInclude(op => op.Product) // Include Product từ OrderItem
                    .Include(or => or.OrderItems)
                    .ThenInclude(op => op.Option)
                    .Select(order => order);

            var result = await order.FirstOrDefaultAsync(order => order.Id == id);
            if (result == null) return null;

            return result.ToOrderDTO();
        }

        public async Task<PagedResult<Order>?> GetOrders(int page, int perPage, string sortBy, string search, string filter)
        {
            var total = await _context.Orders.CountAsync();
            var orders = _context.Orders.AsQueryable();

            #region Searching
            if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                orders = orders.Where(o => o.UserName.Contains(search)
                || o.Address.Contains(search) || o.PhoneNumber.Contains(search));
            #endregion

            #region sorting

            switch (sortBy)
            {
                case "total_asc":
                    orders = orders.OrderBy(o => o.Total);
                    break;
                case "total_desc":
                    orders = orders.OrderByDescending(o => o.Total);
                    break;
                case "name_asc":
                    orders = orders.OrderBy(o => o.UserName);
                    break;
                case "name_desc":
                    orders = orders.OrderByDescending(o => o.UserName);
                    break;
                case "time_asc":
                    orders = orders.OrderBy(o => o.CreatedAt);
                    break;
                case "time_desc":
                    orders = orders.OrderByDescending(o => o.CreatedAt);
                    break;
                default:
                    break;
            }
            #endregion

            #region filter

            switch (filter)
            {
                case "Processing":
                    orders = orders.Where(o => o.Status == "Processing");
                    break;
                case "Confirmed":
                    orders = orders.Where(o => o.Status == "Confirmed");
                    break;
                default:
                    break;
            }

            #endregion

            #region paging
            orders = orders.Skip((page - 1) * perPage).Take(perPage);
            #endregion

            var result = await orders.ToListAsync();
            PagedResult<Order> paging = new PagedResult<Order>(result, total, page, perPage);
            return paging;
        }

        public async Task<List<Order>?> PrepareOrders(int[] OrderIds)
        {
            var orders = await _context.Orders.Where(p => OrderIds.Contains(p.Id)).ToListAsync();
            if (orders.Count() == 0) return null;

            foreach (var item in orders)
            {
                item.Status = "Preparing";
                item.UpdateAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return orders;
        }
        public async Task<List<Order>?> ConfirmOrders(int[] orderIds)
        {
            var orders = await _context.Orders.Where(order => orderIds.Contains(order.Id)).ToListAsync();
            if (orders == null || orders.Count() == 0) return null;
            foreach (var order in orders)
            {
                order.Status = "Confirmed";
                order.UpdateAt = DateTime.Now;
            }
            await _context.SaveChangesAsync();
            return orders;
        }

        public async Task<List<Order>?> DeleteOrders(int[] orderIds)
        {
            var result = new List<Order>();
            var orders = await _context.Orders.Where(o => orderIds.Contains(o.Id)).ToListAsync();
            foreach (var order in orders)
            {
                result.Add(order);
            }
            _context.RemoveRange(orders);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}