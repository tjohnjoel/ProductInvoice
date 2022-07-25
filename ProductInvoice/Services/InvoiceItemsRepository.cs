using ProductInvoice.Models;

namespace ProductInvoice.Services
{
    public class InvoiceItemsRepository : IInvoiceItemsRepository
    {
        private readonly AppDbContext _appDbContext;
        public InvoiceItemsRepository(AppDbContext appDbContext)
        {
            _appDbContext=appDbContext;
        }
        public void AddItems(Guid invoiceId,Product product)
        {
            InvoiceItems invoiceItems= _appDbContext.InvoiceItems.Where(i => i.ProductId == product.ProductId 
            && i.InvoiceId == invoiceId).FirstOrDefault();
            if (invoiceItems == null)
            {
                invoiceItems= new InvoiceItems();
                invoiceItems.ProductId = product.ProductId;
                invoiceItems.ProductName = product.ProductName;
                invoiceItems.NoOfItems = 1;
                invoiceItems.InvoiceId = invoiceId;
                invoiceItems.InvoiceItemsId = Guid.NewGuid();

                _appDbContext.InvoiceItems.Add(invoiceItems);
            }
            else
            {
                invoiceItems.NoOfItems = ++invoiceItems.NoOfItems;
            }
            _appDbContext.SaveChanges();
        }

        public void RemoveItems(Guid invoiceId)
        {
            List<InvoiceItems> invoiceItems = _appDbContext.InvoiceItems.Where(i =>
            i.InvoiceId == invoiceId).ToList();
            foreach(InvoiceItems item in invoiceItems)
            {

                _appDbContext.InvoiceItems.Remove(item);
                _appDbContext.SaveChanges();
            }
        }
    }
}
