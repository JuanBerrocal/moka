using Moka.Models.Enums;

namespace Moka.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public int OriginId { get; set; }
        public int DestinationId { get; set; }
        public DateOnly Date { get; set; }
        public MovementType MovementType { get; set; }
        public string? Process { get; set; }
        public string? DocumentNumber { get; set; }
        public string? Notes { get; set; }
        public bool Temporary { get; set; }
    }
}
