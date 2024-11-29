using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PetsShop_API_DotNet.Models;

namespace petshop.Models
{
  public class Product
  {
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Column("product_name")]
    public string ProductName { get; set; }
    [Column("create_at")]
    public DateTime CreateAt { get; set; } = DateTime.Now;
    [Column("update_at")]
    public DateTime UpdateAt { get; set; } = DateTime.Now;
    [Column("dom_description")]
    public string DOM { get; set; }
    [Column("short_description")]
    public string Description { get; set; }
    public List<Option> Options { get; set; } = new List<Option>();
    public List<ProductImage> Images { get; set; } = new List<ProductImage>();

    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
  }

}