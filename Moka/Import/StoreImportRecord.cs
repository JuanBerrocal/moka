namespace Moka.Import
{
    /// <summary>
    /// A class to save the needed fields from a CSV exportd file.
    /// </summary>
    public class StoreImportRecord
    {
        public string? Name { get; set; }
        public string? SapCode { get; set; } = null;
        public string? TradeName { get; set; } = null;
        public string? Address { get; set; } = null;
        public string? PostalCode { get; set; } = null;
        public string? City { get; set; } = null;
        public string? TaxId { get; set; } = null;
    }
}
