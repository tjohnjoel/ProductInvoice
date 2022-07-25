using ProductInvoice.Models;

namespace ProductInvoice.Services
{
    public interface IInvoiceItemsRepository
    {
        void AddItems(Guid invoiceId, Product product);
        void RemoveItems(Guid invoiceId);
    }
}
