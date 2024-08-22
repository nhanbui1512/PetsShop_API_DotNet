using Microsoft.EntityFrameworkCore;
using petshop.Data;
using PetsShop_API_DotNet.Interfaces;
using PetsShop_API_DotNet.Mappers;
using PetsShop_API_DotNet.Models;

namespace PetsShop_API_DotNet.Repository
{
    public class ImageRepository(AppDbContext context) : IImageRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<List<ProductImage>?> AddImages(List<string> Urls, int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return null;

            var listImage = Urls.ToProductImages(productId: productId);
            _context.ProductImages.AddRange(listImage);
            await _context.SaveChangesAsync();

            var result = new List<ProductImage>();
            foreach (var item in listImage)
            {
                result.Add(new ProductImage
                {
                    Id = item.Id,
                    FileURL = item.FileURL,
                    ProductId = item.ProductId
                });
            }
            return result;
        }
        public async Task<List<ProductImage>?> GetImages(int productId)
        {
            var images = await _context.ProductImages.Where(i => i.ProductId == productId).ToListAsync();
            return images;
        }


        public async Task<bool> Remove(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null) return false;
            _context.ProductImages.Remove(image);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}