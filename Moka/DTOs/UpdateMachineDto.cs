using Moka.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Moka.DTOs
{
    public class UpdateMachineDto
    {
        [Required]
        [StringLength(100)]
        public string Model { get; set; } = string.Empty;
        [StringLength(100)]
        public string? Serial { get; set; }
        [StringLength(8)]
        public string? SapCode { get; set; } = null;
        [StringLength(10)]
        public string? Asset { get; set; }
        [Required]
        public MachineType Type { get; set; } = MachineType.Unknown;
        public DateOnly? PurchaseDate { get; set; }
        [Required]
        public MachineState State { get; set; } = MachineState.Unknown;
        [Required]
        public int StoreId { get; set; }
        [StringLength(100)]
        public string? Assignment { get; set; }
        [Required]
        public bool IsExternal { get; set; }
        public string? Notes { get; set; }
    }
}
