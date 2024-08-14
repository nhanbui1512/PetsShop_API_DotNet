
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using petshop.Dtos.Orders;
using petshop.Interfaces;
using petshop.Models;
using PetsShop_API_DotNet.Dtos.Orders;
using PetsShop_API_DotNet.Interfaces;

namespace petshop.Controllers
{
    [Route("/api/orders")]
    public class OrderController : ControllerBase
    {

        private readonly IOptionRepository _optionRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IBillsRepository _billsRepository;
        public OrderController(IOptionRepository optionRepository, IOrderRepository orderRepository, IBillsRepository billsRepository)
        {
            _optionRepository = optionRepository;
            _orderRepository = orderRepository;
            _billsRepository = billsRepository;
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
                        Price = option.Price.Value,
                        ProductId = option.ProductId
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
            if (newOrder.OrderItems.Count() == 0)
                return NotFound(new { message = "Not found any options", status = StatusCodes.Status404NotFound });

            var res = await _orderRepository.Create(newOrder);
            if (res == null) return BadRequest(new { message = "Not found any options can order" });
            return Ok(new
            {
                data = res
            });
        }

        [HttpGet]
        [Route("{order_id:int}")]
        public async Task<IActionResult> FindOrder([FromRoute, Range(1, int.MaxValue)] int order_id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var order = await _orderRepository.GetById(order_id);
            if (order == null) return NotFound(new { message = "Not Found Order", status = StatusCodes.Status404NotFound });

            return Ok(new { data = order });
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] string? search, [FromQuery] string? sortBy, [FromQuery] string? filter, [FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 100)] int perPage = 10)
        {
            var orders = await _orderRepository.GetOrders(page, perPage, sortBy, search, filter);
            return Ok(orders);
        }
        [HttpPatch]
        [Route("prepare")]
        public async Task<IActionResult> PrepareOrders([FromBody] PrepareOrder data)
        {
            List<int> filterIds = new List<int>();

            foreach (int id in data.OrderIds)
            {
                if (id > 0 && id <= int.MaxValue && !filterIds.Contains(id))
                    filterIds.Add(id);
            }
            var result = await _orderRepository.PrepareOrders(filterIds.ToArray());
            if (result == null) return NotFound(new { message = "Not found any orders", status = StatusCodes.Status404NotFound });
            return Ok(result);
        }
        [HttpDelete]
        [Route("{order_id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute, Range(1, int.MaxValue)] int order_id)
        {
            var result = await _orderRepository.Delete(order_id);
            if (result == null) return NotFound(new { message = "Not found order", status = StatusCodes.Status404NotFound });
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteMultiOrder([FromBody] DeleteMultiOrder data)
        {
            var result = await _orderRepository.DeleteOrders(data.OrderIds);

            return Ok(result);
        }

        [HttpPatch]
        [Route("confirm")]
        public async Task<IActionResult> ConfirmOrders([FromBody] PrepareOrder data)
        {
            var result = await _orderRepository.ConfirmOrders(data.OrderIds.ToArray());
            if (result == null) return NotFound(new { message = "Not found any orders", status = StatusCodes.Status404NotFound });
            var orderIds = result.Select(o => o.Id).ToArray();
            var bills = await _billsRepository.GenerateBills(orderIds);

            return Ok(bills);
        }

    }
}