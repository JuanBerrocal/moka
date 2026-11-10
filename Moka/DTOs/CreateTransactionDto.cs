using Moka.Models;
using Moka.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Moka.DTOs
{
    public class CreateTransactionDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "OriginId must be a positive integer.")]
        public int OriginId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "DestinationId must be a positive integer.")]
        public int DestinationId { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public MovementType MovementType { get; set; }
        [StringLength(40)]
        public string? Process { get; set; }
        [StringLength(20)]
        public string? DocumentNumber { get; set; }
        public string? Notes { get; set; }
        [Required]
        public bool Temporary { get; set; }
        [MinLength(1, ErrorMessage = "A transaction must contain at least one line.")]
        [Required]
        public List<CreateTransactionLineDto> TransactionLines { get; set; } = new List<CreateTransactionLineDto>();
    }

    public class CreateTransactionLineDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "MachineId must be a positive integer.")]
        public int MachineId { get; set; }
        public string? Notes { get; set; }
    }
}
