using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace petshop.Models
{
    public class Option
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("option_name")]
        public string? Name { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("price")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        [Column("create_at")]
        public DateTime CreateAt { get; set; } = DateTime.Now;
        [Column("update_at")]
        public DateTime UpdateAt { get; set; } = DateTime.Now;


        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }

    }
}