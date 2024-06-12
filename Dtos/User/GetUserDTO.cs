using System;
using System.ComponentModel.DataAnnotations;

namespace petshop.Dtos.User
{
    public class GetUserDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Page { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int PerPage { get; set; }
        public string? Sort { get; set; }
        public string? Search { get; set; }
    }
}