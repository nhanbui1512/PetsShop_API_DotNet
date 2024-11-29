using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace petshop.Dtos.Option
{
    public class OptionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        private decimal? _price;

        public decimal? Price
        {
            get
            {
                // Return the floored value if Price has a value, otherwise return null
                return _price.HasValue ? Math.Floor(_price.Value) : (decimal?)null;
            }
            set
            {
                _price = value;
            }
        }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int ProductId { get; set; }

    }
}