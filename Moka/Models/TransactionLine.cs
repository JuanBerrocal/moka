namespace Moka.Models
{
    public class TransactionLine
    {
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; } = null!;
        public int LineId { get; set; }
        public int MachineId { get; set; } = 0;
        public Machine Machine { get; set; } = null!;
        public string? Notes { get; set; } = null;


    }
}
