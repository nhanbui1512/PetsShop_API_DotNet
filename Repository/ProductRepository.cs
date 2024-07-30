
using System.Collections;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Dtos.Product;
using petshop.Interfaces;
using petshop.Mappers;
using petshop.Models;
using PetsShop_API_DotNet.Dtos.Product;
using PetsShop_API_DotNet.Models;

namespace petshop.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext appContext)
        {
            _context = appContext;
        }
        public async Task<PagedResult<GetProductDTO>> GetAllAsync(string search, int page, int limit, string? sortBy)
        {
            var totalCount = await _context.Products.CountAsync();

            var allProducts = _context.Products.AsQueryable();


            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(p => p.ProductName.Contains(search));
            }



            #region Paging
            allProducts = allProducts.Skip((page - 1) * limit).Take(limit);
            #endregion

            #region Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {

                switch (sortBy)
                {
                    case "price_asc":
                        allProducts = allProducts.OrderBy(p => p.Options.Min(o => o.Price));
                        break;
                    case "price_desc":
                        allProducts = allProducts.OrderByDescending(p => p.Options.Max(o => o.Price));
                        break;
                    case "name_asc":
                        allProducts = allProducts.OrderBy(p => p.ProductName);
                        break;
                    case "name_desc":
                        allProducts = allProducts.OrderByDescending(p => p.ProductName);
                        break;
                    case "create_asc":
                        allProducts = allProducts.OrderBy(p => p.CreateAt);
                        break;
                    case "create_desc":
                        allProducts = allProducts.OrderByDescending(p => p.CreateAt);
                        break;
                    default:
                        // code block
                        break;
                }
            }

            #endregion

            var products = await allProducts
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
            }.ToProductDTO())
            .ToListAsync();

            var pagination = new PagedResult<GetProductDTO>(products, totalCount, page, limit);
            return pagination;
        }

        public async Task<GetProductDTO?> GetById(int Id)
        {

            var product = _context.Products.AsQueryable();
            product = product.Include(p => p.Category).Include(p => p.Images).Include(p => p.Options);
            product = product.Select(p => new Product
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                ProductName = p.ProductName,
                Category = p.Category,
                Options = p.Options,
                CreateAt = p.CreateAt,
                UpdateAt = p.UpdateAt,
                Description = p.Description,
                Images = p.Images,
                DOM = p.DOM,
            });

            var result = await product.FirstOrDefaultAsync(p => p.Id == Id);
            if (result == null) return null;
            return result.ToProductDTO();

        }
        public async Task<Product?> AddProduct(CreateProductDTO data)
        {
            List<ProductImage> images = [];
            List<Option> options = [];
            foreach (var item in data.Images)
            {
                images.Add(new ProductImage
                {
                    FileURL = item
                });

            }

            foreach (var item in data.CreateOptionDTOs)
            {
                options.Add(new Option
                {
                    Price = item.Price,
                    Name = item.Name,
                    Quantity = item.Quantity,
                });

            }

            var newProduct = new Product
            {
                ProductName = data.ProductName,
                CategoryId = data.CategoryId,
                Description = data.Description,
                DOM = data.DOMDescription,
                Images = images,
                Options = options
            };
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            return newProduct;
        }

        public async Task<int> CountProductsOfCategory(int categoryId)
        {
            var count = await _context.Products.Where(p => p.CategoryId == categoryId).CountAsync();
            return count;
        }

    }
}