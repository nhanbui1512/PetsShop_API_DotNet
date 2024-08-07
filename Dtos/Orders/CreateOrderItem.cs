
using System.ComponentModel.DataAnnotations;

namespace petshop.Dtos.Orders
{
    public class CreateOrderItem
    {

        [Required]
        [Range(1, int.MaxValue)]
        public int OptionId { get; set; }
        [Required]
        [Range(1, 1000)]
        public int Quantity { get; set; }
        private decimal Price;

        public void SetPrice(decimal price)
        {
            Price = price;
        }
        public decimal GetPrice()
        {
            return Price;
        }

    }
}