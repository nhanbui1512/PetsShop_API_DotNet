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
            var listImage = Urls.ToProductImages(productId: productId);
            _context.ProductImages.AddRange(listImage);
            await _context.SaveChangesAsync();
            return listImage;
        }
    }
}