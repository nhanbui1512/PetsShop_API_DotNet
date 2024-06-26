using System;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Dtos.Option;
using petshop.Interfaces;
using petshop.Mappers;

namespace petshop.Repository
{
    public class OptionRepository : IOptionRepository

    {
        private readonly AppDbContext _context;
        public OptionRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<OptionDTO> Add(CreateOptionDTO data)
        {
            throw new NotImplementedException();
        }

        public Task<List<OptionDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OptionDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OptionDTO>> GetByProductId(int productId)
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

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<OptionDTO> Update(UpdateOptionDTO data, int id)
        {
            var option = await _context.Options.FindAsync(id);
            if (option == null) return null;

            if (data.Name != null) option.Name = data.Name;
            if (data.Price != null) option.Price = data.Price;
            if (data.Quantity != null) option.Quantity = data.Quantity;
            option.UpdateAt = DateTime.Now;
            return option.ToOptionDTO();

        }
    }
}