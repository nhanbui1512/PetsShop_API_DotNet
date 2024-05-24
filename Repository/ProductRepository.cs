
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
        public Task<List<Product>> GetAllAsync()
        {
            return _context.Products.Include(product => product.Category).Include(product => product.Options).ToListAsync();
        }
    }
}