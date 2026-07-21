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
        [StringLength(100)]
        public string? TradeName { get; set; } = null;

        [StringLength(100)]
        public string? Address { get; set; } = null;

        [StringLength(12)]
        public string? PostalCode { get; set; } = null;

        [StringLength(100)]
        public string? City { get; set; } = null;

        [StringLength(20)]
        public string? TaxId { get; set; } = null;

        public string? Notes { get; set; } = null;
    }
}
