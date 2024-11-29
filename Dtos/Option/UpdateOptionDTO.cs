using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace petshop.Dtos.Option
{
    public class UpdateOptionDTO
    {
        public string Name { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(typeof(decimal), "1.0", "79228162514264337593543950335")]
        public decimal? Price { get; set; }

    }
}