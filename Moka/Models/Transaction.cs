using Moka.Models.Enums;

namespace Moka.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public Machine Machine { get; set; } = null!;
        public int OriginId { get; set; }
        public Store Origin { get; set; } = null!;
        public int DestinationId { get; set; }
        public Store Destination { get; set; } = null!;
        public DateOnly Date { get; set; }
        public MovementType MovementType { get; set; }
        public string? Process { get; set; }
        public string? DocumentNumber { get; set; }
        public string? Notes { get; set; }
        public bool Temporary { get; set; }
    }
}
