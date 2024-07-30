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
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public decimal? Price
        {
            get
            {
                return Math.Floor(Price.Value);
            }
            set
            {
                Price = value;
            }
        }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int ProductId { get; set; }

    }
}