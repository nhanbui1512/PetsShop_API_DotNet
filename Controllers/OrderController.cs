
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualBasic;
using petshop.Dtos.Option;
using petshop.Dtos.Orders;
using petshop.Interfaces;
using petshop.Models;

namespace petshop.Controllers
{
    [Route("/api/orders")]
    public class OrderController : ControllerBase
    {

        private readonly IOptionRepository _optionRepository;
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOptionRepository optionRepository, IOrderRepository orderRepository)
        {
            _optionRepository = optionRepository;
            _orderRepository = orderRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrder data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // gom các object có id trùng nhau và cộng số lượng vào với nhau
            data.Items = data.Items
           .GroupBy(item => item.OptionId)
           .Select(group => new CreateOrderItem
           {
               OptionId = group.Key,
               Quantity = group.Aggregate(0, (acc, item) => acc + item.Quantity)
               // acc is the last return value , item is element of each loop
               // or can write: quantity = group.Sum(item => item.Quantity)
           })
           .ToList();

            int[] ids = data.Items.Select(o => o.OptionId).ToArray(); // ids of orderItems
            var options = await _optionRepository.GetOptionsByIds(ids);
            var existIds = options.Select(o => o.Id).ToArray();

            var orders = new List<OrderItem>();

            // get order items if existed
            foreach (var item in data.Items)
            {
                var option = options.Find(o => o.Id == item.OptionId);
                if (option != null)
                {
                    orders.Add(new OrderItem
                    {
                        Quantity = item.Quantity,
                        OptionId = item.OptionId,
                        Price = option.Price.Value
                    });
                }
            }

            var newOrder = new Order
            {
                Address = data.Address,
                UserName = data.UserName,
                PhoneNumber = data.PhoneNumber,
                OrderItems = orders,
                Total = orders.Sum(o => o.Price * o.Quantity)
            };

            newOrder = await _orderRepository.Create(newOrder);

            return Ok(new
            {
                newOrder
            });
        }
    }
}