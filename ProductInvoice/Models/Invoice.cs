namespace ProductInvoice.Models
{
    public class Invoice
    {
        public Guid InvoiceId { get; set; }
        public DateTime InvoiceCreated { get; set; }
        public decimal TotalPrice { get; set; }
        public List<InvoiceItems> invoiceItems { get; set; }
    }
}
