using System.ComponentModel.DataAnnotations;
using Moka.Migrations;

namespace Moka.DTOs
{
    public class CreateStoreDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(20)]
        public string? SapCode { get; set; }
    }
}
