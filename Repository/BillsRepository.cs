using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Models;
using PetsShop_API_DotNet.Interfaces;

namespace PetsShop_API_DotNet.Repository
{
    public class BillsRepository(AppDbContext context) : IBillsRepository

    {
        private readonly AppDbContext _context = context;
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
    }
}