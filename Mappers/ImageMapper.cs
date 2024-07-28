using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetsShop_API_DotNet.Models;

namespace PetsShop_API_DotNet.Mappers
{
    public static class ImageMapper
    {
        public static List<string> ToImageUrls(this List<ProductImage> listImage)
        {

            List<string> result = new List<string>();
            foreach (var item in listImage)
            {
                result.Add(item.FileURL);
            }
            return result;
        }

        public static List<ProductImage> ToProductImages(this List<string> arr, int productId)
        {
            List<ProductImage> result = new List<ProductImage>();
            foreach (var item in arr)
            {
                result.Add(new ProductImage
                {
                    FileURL = item,
                    ProductId = productId
                });
            }
            return result;
        }

    }
}