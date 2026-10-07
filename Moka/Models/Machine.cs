using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Moka.Models.Enums;

namespace Moka.Models
{
    public class Machine
    {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public string? Serial { get; set; } = string.Empty;
        public string? SapCode { get; set; }
        public string? Asset { get; set; }
        public MachineType Type { get; set; } = MachineType.Unknown;
        public DateOnly? PurchaseDate { get; set; }
        public MachineState State { get; set; } = MachineState.Unknown;
        public int StoreId { get; set; }
        public string? Assignment { get; set; }
        public bool IsExternal { get; set; }
        public string? Notes { get; set; } = null;
    }
    
}
