using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Dtos.Category;
using petshop.Dtos.Product;
using petshop.Interfaces;
using petshop.Mappers;
using petshop.Models;
using PetsShop_API_DotNet.Dtos.Product;

namespace petshop.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            this._context = context;
        }


        public async Task<CategoryDTO> Add(CreateCategoryDTO data)
        {

            var newCategory = new Category
            {
                CategoryName = data.CategoryName,
                Description = data.Description,
            };

            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            return new CategoryDTO
            {
                Id = newCategory.Id,
                CategoryName = newCategory.CategoryName,
                Description = newCategory.Description,
                CreatedAt = newCategory.CreateAt,
                UpdatedAt = newCategory.UpdateAt
            };
        }

        public async Task<List<CategoryDTO>> GetAll()
        {
            var categories = await _context.Categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                Description = c.Description,
                CreatedAt = c.CreateAt,
                UpdatedAt = c.UpdateAt
            }).ToListAsync();

            return categories;
        }

        public async Task<CategoryDTO?> GetById(int id, int page, int limit)
        {

            var result = await _context.Categories
            .Include(c => c.Products)
            .ThenInclude(p => p.Options)
            .FirstOrDefaultAsync(c => c.Id == id);

            if (result == null) return null; // Trả về null nếu không tìm thấy Category
            result.Products = result.Products
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToList();

            foreach (var product in result.Products)
            {
                product.Options = product.Options
                    .Select(o => new Option
                    {
                        Id = o.Id,
                        Name = o.Name,
                        Price = o.Price,
                        Quantity = o.Quantity,
                        ProductId = o.ProductId
                    }).ToList();
            }

            var products = result.Products.Select(product => new GetProductDTO
            {
                Id = product.Id,
                ProductName = product.ProductName,
                CreateAt = product.CreateAt,
                UpdateAt = product.UpdateAt,
                Description = product.Description,
                Options = product.Options,
                DOM = product.DOM,

            }).ToList();
            return new CategoryDTO { Id = result.Id, CategoryName = result.CategoryName, CreatedAt = result.CreateAt, UpdatedAt = result.UpdateAt, Products = products };
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryDTO?> Update(UpdateCategoryDTO data, int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            if (data.CategoryName != null) category.CategoryName = data.CategoryName;
            if (data.Description != null) category.Description = data.Description;
            category.UpdateAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return new CategoryDTO
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                Description = category.Description,
                CreatedAt = category.CreateAt,
                UpdatedAt = category.UpdateAt,
            };

        }
    }
}