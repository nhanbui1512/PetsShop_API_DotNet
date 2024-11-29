
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using petshop.Models;


namespace PetsShop_API_DotNet.Models
{
    public class RefreshToken
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [NotNull]
        [Column("token_value")]
        public string TokenValue { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("expire_time")]
        public DateTime ExpireTime { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}