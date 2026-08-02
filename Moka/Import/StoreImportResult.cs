namespace Moka.Import
{
    public class StoreImportResult
    {
        public string FileName { get; set; } = string.Empty;
        public int Created { get; set; }
        public int Updated { get; set; }
        public int Skipped { get; set; }
        public List<string> Errors { get; set; } = new List<string>();  
    }
}
