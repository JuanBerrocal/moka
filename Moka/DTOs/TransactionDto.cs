using Moka.Models;
using Moka.Models.Enums;

namespace Moka.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int OriginId { get; set; }
        public int DestinationId { get; set; }
        public DateOnly Date { get; set; }
        public MovementType MovementType { get; set; }
        public string? Process { get; set; }
        public string? DocumentNumber { get; set; }
        public string? Notes { get; set; }
        public bool Temporary { get; set; }
        public List<TransactionLineDto> TransactionLines { get; set; } = new List<TransactionLineDto>();
        
    }

    public class TransactionLineDto
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int MachineId { get; set; }
        public string? Notes { get; set; }
    }
}
