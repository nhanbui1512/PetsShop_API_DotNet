
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using petshop.Models;

namespace PetsShop_API_DotNet.Models
{
  public class Blog
  {
    [Column("id")]
    [Key]
    public int Id { get; set; }
    [Column("title")]
    public string Title { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("author")]
    public string Author { get; set; }
    [Column("DOM")]
    public string DOM { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [NotMapped]
    public string CreatedAtStr
    {
      get { return $"{CreatedAt.ToLongTimeString()} {CreatedAt.ToLongDateString()}"; }
    }
    [NotMapped]
    public string UpdatedAtStr
    {
      get { return $"{UpdatedAt.ToLongTimeString()} {UpdatedAt.ToLongDateString()}"; }
    }

  }
}