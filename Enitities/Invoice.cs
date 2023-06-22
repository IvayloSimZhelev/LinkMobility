namespace Enitities
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public string? InvoiceNumber { get; set; }
        public DateTime? Date { get; set; }
        public decimal Total { get; set; }
    }
}