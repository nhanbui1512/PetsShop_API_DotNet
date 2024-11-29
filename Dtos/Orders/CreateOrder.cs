
using System.ComponentModel.DataAnnotations;
using petshop.Validations;
namespace petshop.Dtos.Orders
{
    public class CreateOrder
    {
        [Required]
        [MinLength(10)]
        [MaxLength(100)]
        public string UserName { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Address { get; set; }
        [Required]
        [MaxLength(11)]
        public string PhoneNumber { get; set; }
        [Required]
        [MinimumItems(1)]
        [MaximumItems(100)]
        public List<CreateOrderItem> Items { get; set; }

    }
}