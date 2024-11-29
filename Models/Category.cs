using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace petshop.Models
{
    public class Category
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("category_name")]
        public string CategoryName { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("create_at")]
        public DateTime CreateAt { get; set; } = DateTime.Now;
        [Column("update_at")]
        public DateTime UpdateAt { get; set; } = DateTime.Now;
        public List<Product> Products { get; set; } = new List<Product>();

    }
}