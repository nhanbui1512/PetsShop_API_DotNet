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

        public static OptionDTO ToOptionDTO(this Option option)
        {
            return new OptionDTO
            {
                Id = option.Id,
                Name = option.Name,
                Quantity = option.Quantity,
                Price = option.Price,
                UpdateAt = option.UpdateAt,
                CreatedAt = option.CreateAt,
                ProductId = option.ProductId
            };
        }
    }
}