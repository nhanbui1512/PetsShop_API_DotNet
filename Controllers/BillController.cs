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
  }
}
