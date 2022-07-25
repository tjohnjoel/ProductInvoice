namespace ProductInvoice.Models
{
    public class InvoiceItems
    {
        public Guid InvoiceItemsId { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int NoOfItems { get; set; }
       
    }
}
