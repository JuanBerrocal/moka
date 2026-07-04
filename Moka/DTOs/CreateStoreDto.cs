using Moka.Migrations;

namespace Moka.DTOs
{
    public class CreateStoreDto
    {
        public string Name { get; set; } = string.Empty;
        public string? SapCode { get; set; }
    }
}
