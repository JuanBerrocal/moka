using Moka.Models.Enums;

namespace Moka.DTOs
{
    public class MachineDto
    {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Serial { get; set; } = string.Empty;
        public string? SapCode { get; set; } = null;
        public string Asset { get; set; } = string.Empty;
        public MachineType Type { get; set; } = MachineType.Unknown;
        public DateOnly? PurchaseDate { get; set; }
        public MachineState State { get; set; } = MachineState.Unknown;
        public int StoreId { get; set; }
        public string Assignment { get; set; } = string.Empty;
        public bool IsExternal { get; set; }
        public string? Notes { get; set; } = null;
    }
}
