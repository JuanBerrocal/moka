using System.ComponentModel.DataAnnotations;

namespace Moka.DTOs
{
    public class UpdateStoreDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [StringLength(20)]
        public string? SapCode { get; set; }
    }
}
