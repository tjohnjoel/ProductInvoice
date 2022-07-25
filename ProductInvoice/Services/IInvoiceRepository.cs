using ProductInvoice.Models;

namespace ProductInvoice.Services
{
    public interface IInvoiceRepository
    {
        IEnumerable<Invoice> invoices();
        void AddInvoice(Guid invoiceId);
        Invoice GetInvoiceById(Guid invoiceId);
        void Cancel(Guid invoiceId);
        decimal UpdatePrice(Product product, Guid invoicId);
        decimal SubtractPrice(Product product, Guid invoicId);
    }
}
