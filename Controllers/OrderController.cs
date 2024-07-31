
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualBasic;
using petshop.Dtos.Option;
using petshop.Dtos.Orders;
using petshop.Interfaces;

namespace petshop.Controllers
{
    [Route("/api/orders")]
    public class OrderController : ControllerBase
    {

        private readonly IOptionRepository _optionRepository;
        public OrderController(IOptionRepository optionRepository)
        {
            _optionRepository = optionRepository;
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

            var orders = data.Items.Where(i => existIds.Contains(i.OptionId)).ToList(); // ids of option that exist in database


            return Ok(new
            {
                data = data,
                orders = orders
            });
        }
    }
}