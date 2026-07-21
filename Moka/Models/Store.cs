namespace Moka.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? SapCode { get; set; } = null;
        public string? TradeName {  get; set; } = null;
        public string? Address { get; set; } = null;
        public string? PostalCode { get; set; } = null;
        public string? City { get; set; } = null;
        public string? TaxId { get; set; } = null;
        public string? Notes { get; set; } = null;

    }
}
