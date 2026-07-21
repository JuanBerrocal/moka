namespace Moka.DTOs
{
    public class StoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? SapCode { get; set; }
        public string? TradeName { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? TaxId { get; set; }
        public string? Notes { get; set; }
    }
}
