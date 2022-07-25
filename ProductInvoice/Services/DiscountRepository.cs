using Microsoft.EntityFrameworkCore;
using ProductInvoice.Models;

namespace ProductInvoice.Services
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly AppDbContext _appDbContext;

        public DiscountRepository(AppDbContext appDbContext)
        {
            _appDbContext=appDbContext;
        }
        public void AddDiscount(Discount discount)
        {
            discount.DiscountId = Guid.NewGuid();
            discount.DiscountCreated = DateTime.Now;

            _appDbContext.Discounts.Add(discount);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Discount> discounts()
        {
            var discountList = _appDbContext.Discounts;
           
             return discountList;
        }
    }
}
