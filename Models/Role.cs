
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using petshop.Models;

namespace PetsShop_API_DotNet.Models
{
    public class Role
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("role_name")]
        public string RoleName { get; set; }
        [Column("create_at")]
        public DateTime CreateAt { get; set; }
        [Column("update_at")]
        public DateTime UpdateAt { get; set; }
        public List<User> Users { get; set; } = new List<User>();

    }
}