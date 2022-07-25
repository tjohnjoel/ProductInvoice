namespace ProductInvoice.Models
{
    public class Discount
    {
        public Guid DiscountId { get; set; }
        public Guid ProductId { get; set; }
        public int DiscountPercent { get; set; }
        public DateTime DiscountCreated { get; set; }
        public DateTime? DiscountExpiration { get; set; }
    }
}
