using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetsShop_API_DotNet.Models;

namespace PetsShop_API_DotNet.Interfaces
{
    public interface IImageRepository
    {
        Task<List<ProductImage>> AddImages(List<string> Urls, int productId);
        Task<List<ProductImage>> GetImages(int productId);
        Task<ProductImage> UpdateImage(string filePath, int imageId);
        Task<bool> Remove(int imageId);
    }
}