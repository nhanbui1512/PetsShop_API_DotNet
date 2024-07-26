using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using petshop.Models;

namespace PetsShop_API_DotNet.Models
{
    public class ProductImage
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("file_url")]
        public string? FileURL { get; set; }
        public int ProductId { get; set; }
    }
}