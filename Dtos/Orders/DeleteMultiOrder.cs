
using System.ComponentModel.DataAnnotations;
using petshop.Validations;
namespace petshop.Dtos.Orders
{
    public class DeleteMultiOrder
    {
        [Required]
        [MinimumItems(1)]
        public int[] OrderIds { get; set; }
    }
}