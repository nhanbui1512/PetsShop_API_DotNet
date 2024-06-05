using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Dtos.Category;
using petshop.Dtos.Product;
using petshop.Interfaces;

namespace petshop.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            this._context = context;
        }
        public Task<CategoryDTO> Add(CreateCategoryDTO data)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryDTO>> GetAll()
        {
            var categories = await _context.Categories.Select(c => new CategoryDTO
            {
                CategoryName = c.CategoryName,
                Description = c.Description,
                CreatedAt = c.CreateAt,
                UpdatedAt = c.UpdateAt
            }).ToListAsync();

            return categories;
        }

        public Task<CategoryDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(UpdateCategoryDTO data)
        {
            throw new NotImplementedException();
        }
    }
}