
using petshop.Dtos.Orders;
using petshop.Models;
using PetsShop_API_DotNet.Dtos.Helpers;

namespace PetsShop_API_DotNet.Mappers
{
    public static class OrderMappers
    {
        public static OrderDTO ToOrderDTO(this Order order)
        {


            var result = new OrderDTO
            {
                Id = order.Id,
                UserName = order.UserName,
                PhoneNumber = order.PhoneNumber,
                Address = order.Address,
                Total = order.Total,
                CreatedAt = order.CreatedAt,
                UpdateAt = order.UpdateAt,
                Status = order.Status,
                OrderItems = order.OrderItems.Select(orItem =>
                    new OrderItem
                    {
                        Id = orItem.Id,
                        Quantity = orItem.Quantity,
                        Price = orItem.Price,
                        OrderId = orItem.OrderId,
                        OptionId = orItem.OptionId,
                        // Product = new Product
                        // {
                        //     Id = orItem.Product.Id,
                        //     ProductName = orItem.Product.ProductName,
                        //     CreateAt = orItem.Product.CreateAt,
                        //     UpdateAt = orItem.Product.UpdateAt,
                        //     CategoryId = orItem.Product.CategoryId,
                        // },
                        // Option = new Option
                        // {
                        //     Id = orItem.Option.Id,
                        //     Name = orItem.Option.Name,
                        //     Quantity = orItem.Option.Quantity,
                        //     Price = orItem.Option.Price,
                        //     CreateAt = orItem.Option.CreateAt,
                        //     UpdateAt = orItem.Option.UpdateAt,
                        //     ProductId = orItem.Option.ProductId
                        // },
                        Product = orItem.Product,
                        Option = orItem.Option

                    })
                    .ToList()
            };

            foreach (var item in result.OrderItems)
            {
                item.Option.SetPropertiesToNull<Option>(["Product"]);
                item.Product.SetPropertiesToNull<Product>(["DOM"]);

            }

            return result;
        }



    }
}