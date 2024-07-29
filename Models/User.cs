using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PetsShop_API_DotNet.Models;

namespace petshop.Models
{
  public class User
  {
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("first_name")]
    // [JsonPropertyName("FirstName")]
    public string? FirstName { get; set; }
    [Column("last_name")]
    public string? LastName { get; set; }
    [Column("email")]
    public string? Email { get; set; }
    [Column("password")]
    public string? Password { get; set; }
    [Column("avatar")]
    public string? Avatar { get; set; } = "DefaultAvatar.png";

    [Column("access_token")]
    public string? AccessToken { get; set; }

    [Column("refresh_token")]
    public string? RefreshToken { get; set; }

    [Column("gender")]
    public bool Gender { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public int RoleId { get; set; }
    public Role? Role { get; set; }
    [NotMapped]
    public string FullName
    {
      get { return $"{FirstName} {LastName}"; }
    }

  }

}