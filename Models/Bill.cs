using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace petshop.Models
{
  [Table("payments")]
  public class Bill
  {
    [Column("id")]
    [Key]
    public int Id { get; set; }
    [Column("total")]
    public decimal Total { get; set; }
    [Column("created_at")]
    public DateTime PayAt { get; set; } = DateTime.Now;
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
    public Order? Order { get; set; }
    public int OrderId { get; set; }
  }
}