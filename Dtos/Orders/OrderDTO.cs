using petshop.Models;

namespace petshop.Dtos.Orders
{
  public class OrderDTO
  {
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdateAt { get; set; }
    public string Status { get; set; } = "Processing";
    public List<OrderItem>? OrderItems { get; set; }
  }
}