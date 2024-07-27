
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Interfaces;
using petshop.Models;

namespace petshop.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext appContext)
        {
            _context = appContext;
        }
        public Task<List<Product>> GetAllAsync(string search, int page, int limit)
        {
            var allProducts = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(p => p.ProductName.Contains(search));
            }

            #region Paging
            allProducts = allProducts.Skip((page - 1) * limit).Take(limit);
            #endregion

            return allProducts
            .Include(product => product.Category)
            .Include(product => product.Options)
            .Include(p => p.Images)
            .Select(p => new Product
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Category = p.Category,
                Options = p.Options,
                CreateAt = p.CreateAt,
                UpdateAt = p.UpdateAt,
                Description = p.Description,
                Images = p.Images
            })
            .ToListAsync();
        }
    }
}