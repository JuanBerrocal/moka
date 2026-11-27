
using Moka.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Moka.DTOs
{
    public class TransactionQueryDto
    {
        public int? OriginId { get; set; }
        public int? DestinationId { get; set; }

        [EnumDataType(typeof(MovementType))]
        public MovementType? MovementType { get; set; }

        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }

        public bool? Temporary { get; set; }

        public string SortBy { get; set; } = "date";
        public bool SortDescending { get; set; } = true;

        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Range(1, 100)]
        public int PageSize { get; set; } = 20;
    }
}
