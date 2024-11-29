using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using petshop.Models;

namespace PetsShop_API_DotNet.Dtos.Product
{
    public class GetProductDTO
    {

        public int Id { get; set; }
        public string ProductName { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string DOM { get; set; }
        public string Description { get; set; }
        public List<Option> Options { get; set; } = new List<Option>();
        public List<string> Images { get; set; } = new List<string>();

        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}