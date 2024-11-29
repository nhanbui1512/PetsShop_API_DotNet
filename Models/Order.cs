using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace petshop.Models
{
    public class Order
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("user_name")]
        public string UserName { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("total")]
        public decimal Total { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Column("updated_at")]
        public DateTime UpdateAt { get; set; }
        [Column("status")]
        public string Status { get; set; } = "Processing";
        public List<OrderItem> OrderItems { get; set; }

        [NotMapped]
        public string CreatedAtStr
        {
            get { return $"{CreatedAt.ToLongTimeString()} {CreatedAt.ToLongDateString()}"; }
        }
        [NotMapped]
        public string UpdatedAtStr
        {
            get { return $"{UpdateAt.ToLongTimeString()} {UpdateAt.ToLongDateString()}"; }
        }

    }
}