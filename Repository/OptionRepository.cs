using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Dtos.Option;
using petshop.Interfaces;
using petshop.Mappers;
using petshop.Models;

namespace petshop.Repository
{
    public class OptionRepository : IOptionRepository

    {
        private readonly AppDbContext _context;
        public OptionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<OptionDTO?> Add(CreateOptionDTO data, int? productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return null;

            var newOption = new Models.Option
            {
                Product = product,
                Name = data.Name,
                Price = data.Price,
                Quantity = data.Quantity,
                ProductId = product.Id
            };
            await _context.Options.AddAsync(newOption);
            await _context.SaveChangesAsync();

            return newOption.ToOptionDTO();
        }

        public Task<List<OptionDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<OptionDTO?> GetById(int id)
        {
            var option = await _context.Options.FindAsync(id);
            if (option == null) return null;
            return option.ToOptionDTO();
        }

        public async Task<List<OptionDTO>?> GetByProductId(int productId)
        {

            var product = await _context.Products.Include(p => p.Options).FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null) return null;

            List<OptionDTO> data = new List<OptionDTO>();

            foreach (var option in product.Options)
            {
                data.Add(new OptionDTO
                {
                    Id = option.Id,
                    Name = option.Name,
                    Price = option.Price,
                    Quantity = option.Quantity,
                    CreatedAt = option.CreateAt,
                    UpdateAt = option.UpdateAt
                });
            }
            return data;
        }

        public async Task<bool> Remove(int id)
        {
            var option = await _context.Options.FirstOrDefaultAsync(o => o.Id == id);
            if (option == null) return false;
            _context.Options.Remove(option);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<OptionDTO?> Update(UpdateOptionDTO data, int id)
        {
            var option = await _context.Options.FindAsync(id);
            if (option == null) return null;

            if (data.Name != null) option.Name = data.Name;
            if (data.Price.HasValue) option.Price = data.Price;
            if (data.Quantity != 0) option.Quantity = data.Quantity;
            option.UpdateAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return option.ToOptionDTO();

        }
        public async Task<List<OptionDTO>?> GetOptionsByIds(int[] ids)
        {
            var options = await _context.Options.Where(o => ids.Contains(o.Id)).Select(o => o.ToOptionDTO()).ToListAsync();
            return options;
        }

        public async Task<List<OptionDTO>?> DecreaseQuantity(List<OrderItem> orderItems)
        {
            var ids = orderItems.Select(o => o.OptionId).ToArray();
            var options = await _context.Options.Where(o => ids.Contains(o.Id)).ToListAsync();
            if (options == null || options.Count == 0) return null;

            var updatedOptions = new List<OptionDTO>();
            foreach (var orderitem in orderItems)
            {
                var option = options.Find(o => o.Id == orderitem.OptionId);
                if (option != null)
                {
                    if (orderitem.Quantity > option.Quantity) continue;
                    option.Quantity -= orderitem.Quantity;
                    updatedOptions.Add(option.ToOptionDTO());
                }
            }
            if (updatedOptions.Count == 0) return null;
            await _context.SaveChangesAsync();
            return updatedOptions;
        }

        public Task<List<Option>?> UpdateOptions(List<Option> data)
        {
            var optionIds = data.Select(o => o.Id).ToArray();
            var options = _context.Options.Where(o => optionIds.Contains(o.Id));

            throw new NotImplementedException();
        }
    }
}