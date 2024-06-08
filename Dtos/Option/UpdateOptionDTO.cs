using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petshop.Dtos.Option
{
    public class UpdateOptionDTO
    {
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}