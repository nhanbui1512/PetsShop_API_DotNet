using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace petshop.Models
{
  public class Product
  {
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("product_name")]
    public string? ProductName { get; set; }
    [Column("create_at")]
    public DateTime CreateAt { get; set; } = new DateTime();
    [Column("update_at")]
    public DateTime UpdateAt { get; set; } = new DateTime();
    public List<Option> Options { get; set; } = new List<Option>();
  }

}