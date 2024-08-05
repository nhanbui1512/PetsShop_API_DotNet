
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
                        ProductId = orItem.ProductId,
                        OptionId = orItem.OptionId,
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