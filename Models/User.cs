using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace petshop.Models
{
  public class User
  {
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("first_name")]
    public string? FirstName { get; set; }
    [Column("last_name")]
    public string? LastName { get; set; }
    [Column("email")]
    public string? Email { get; set; }
    [Column("password")]
    public string? Password { get; set; }
    [Column("avatar")]
    public string? Avatar { get; set; } = "DefaultAvatar.png";

    [Column("gender")]
    public bool Gender { get; set; }
  }

}