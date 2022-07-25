using ProductInvoice.Models;

namespace ProductInvoice.Services
{
    public interface IDiscountRepository
    {
        IEnumerable<Discount> discounts();
        void AddDiscount(Discount discount);
        
    }
}
