using Microsoft.EntityFrameworkCore;
using ProductInvoice.Models;

namespace ProductInvoice.Services
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _appDbContext;
        public InvoiceRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Invoice> invoices()
        {
            return _appDbContext.Invoices.Include(i => i.invoiceItems);    
        }
        public Invoice GetInvoiceById(Guid invoiceId)
        {
            return _appDbContext.Invoices.Where(i => i.InvoiceId == invoiceId).FirstOrDefault();
        }
        public void AddInvoice(Guid invoiceId)
        {
            Invoice invoice = new Invoice();
            invoice.InvoiceId=invoiceId;
            invoice.InvoiceCreated = DateTime.Now;
            invoice.TotalPrice = 0;
            _appDbContext.Invoices.Add(invoice);
            _appDbContext.SaveChanges();
        }
        public void Cancel(Guid invoiceId)
        {
            
            _appDbContext.Invoices.Remove(GetInvoiceById(invoiceId));
            _appDbContext.SaveChanges();
        }

        public decimal UpdatePrice(Product product, Guid invoicId)
        {
            decimal Cost = product.ProductBasePrice;
            Discount discount = _appDbContext.Discounts.FirstOrDefault(i => i.ProductId == product.ProductId);
            
            Invoice invoice = GetInvoiceById(invoicId);
            if (discount != null)
            {
                decimal disCostPerc = discount.DiscountPercent;
                decimal savings = Cost * disCostPerc / 100;
                
                invoice.TotalPrice = invoice.TotalPrice + Cost - savings;
            }
            else
            {
                invoice.TotalPrice = invoice.TotalPrice + Cost;
            }
            

            _appDbContext.SaveChanges();
            return invoice.TotalPrice;
        }

        public decimal SubtractPrice(Product product, Guid invoiceId)
        {
            decimal Cost = product.ProductBasePrice;
            Discount discount = _appDbContext.Discounts.FirstOrDefault(i => i.ProductId == product.ProductId);

            Invoice invoice = GetInvoiceById(invoiceId);
            InvoiceItems invoiceItems = _appDbContext.InvoiceItems.Where(i => i.ProductId == product.ProductId
            && i.InvoiceId == invoiceId).FirstOrDefault();
            if (invoiceItems == null)
            {
                return 0;
            }
            if (discount != null)
            {
                decimal disCostPerc = discount.DiscountPercent;
                decimal savings = Cost * disCostPerc / 100;

                Cost = Cost - savings;

                invoice.TotalPrice = invoice.TotalPrice-Cost;
            }
            else
            {
                invoice.TotalPrice = invoice.TotalPrice - Cost;
            }


            _appDbContext.SaveChanges();
            return invoice.TotalPrice;
        }
    }
}
