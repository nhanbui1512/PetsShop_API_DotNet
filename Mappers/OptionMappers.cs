using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using petshop.Dtos.Option;
using petshop.Models;

namespace petshop.Mappers
{
    public static class OptionMappers
    {
        public static Option ToOptionObject(this CreateOptionDTO createOption)
        {
            return new Option
            {
                Name = createOption.Name,
                Quantity = createOption.Quantity,
                Price = createOption.Price,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            };
        }
    }
}