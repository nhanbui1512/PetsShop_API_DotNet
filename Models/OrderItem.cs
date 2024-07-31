using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace petshop.Models
{
    public class OrderItem
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("price")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public decimal MyProperty { get; set; }

        public Option? Option { get; set; }
        public int OptionId { get; set; }

    }
}