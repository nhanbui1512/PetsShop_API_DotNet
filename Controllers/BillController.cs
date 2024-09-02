using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PetsShop_API_DotNet.Interfaces;

namespace petshop.Controllers
{

  [Route("/api/bills")]
  [ApiController]
  public class BillController : ControllerBase
  {

    private readonly IBillsRepository _billsRepository;
    public BillController(IBillsRepository billsRepository)
    {
      _billsRepository = billsRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetBills()
    {
      var bills = await _billsRepository.GetBills();
      return Ok(bills);
    }

    [HttpGet]
    [Route("{bill_id}")]
    public async Task<IActionResult> GetBillById([FromRoute, Range(1, int.MaxValue)] int bill_id)
    {
      var bill = await _billsRepository.GetById(bill_id);
      if (bill == null) return NotFound(new { message = "Not found bill", status = 404 });
      return Ok(bill);
    }

    [HttpDelete]
    [Route("{bill_id}")]
    public async Task<IActionResult> DeleteBill([FromRoute, Range(1, int.MaxValue)] int bill_id)
    {
      var result = await _billsRepository.Delete(bill_id);
      if (result == null) return NotFound(new { message = "Not found bill", status = 404 });
      return Ok();
    }
  }
}
