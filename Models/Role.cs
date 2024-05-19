
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PetsShop_API_DotNet.Models
{
    public class Role
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("role_name")]
        public string? RoleName { get; set; }
        [Column("create_at")]
        public DateTime CreateAt { get; set; }
        [Column("update_at")]
        public DateTime UpdateAt { get; set; }

    }
}