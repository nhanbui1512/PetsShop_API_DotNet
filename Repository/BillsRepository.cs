using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Models;
using PetsShop_API_DotNet.Dtos.Helpers;
using PetsShop_API_DotNet.Interfaces;

namespace PetsShop_API_DotNet.Repository
{
    public class BillsRepository(AppDbContext context) : IBillsRepository

    {
        private readonly AppDbContext _context = context;

        public async Task<bool?> Delete(int bill_id)
        {
            var bill = await _context.Bills.FirstOrDefaultAsync(b => b.Id == bill_id);
            if (bill == null) return null;
            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<List<Bill>?> GenerateBills(int[] orderIds)
        {
            var orders = await _context.Orders.Where(o => orderIds.Contains(o.Id)).ToListAsync();
            if (orders == null || orders.Count() == 0) return null;
            List<Bill> newBills = new List<Bill>();
            foreach (var order in orders)
            {
                newBills.Add(new Bill
                {
                    Total = order.Total,
                    OrderId = order.Id,
                });
            }
            _context.Bills.AddRange(newBills);
            await _context.SaveChangesAsync();
            return newBills;
        }

        public async Task<List<Bill>?> GetBills()
        {
            var bills = await _context.Bills.ToListAsync();
            return bills;
        }

        public async Task<Bill?> GetById(int billId)
        {
            var bill = context.Bills.AsQueryable();

            bill = bill.Include(b => b.Order)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .Include(b => b.Order)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Option);

            var result = await bill.FirstOrDefaultAsync();

            foreach (var item in result.Order.OrderItems)
            {
                item.Product.SetPropertiesToNull<Product>(["DOM"]);
            }
            if (result == null) return null;
            return result;
        }


    }
}