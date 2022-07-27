using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductInvoice.Models
{
    public class Discount
    {
        public Guid DiscountId { get; set; }
        public Guid ProductId { get; set; }
        [Required]
        [DisplayName("Percentage")]
        public int DiscountPercent { get; set; }
        public DateTime DiscountCreated { get; set; }
        [Required]
        [DisplayName("Expiration Date")]
        public DateTime? DiscountExpiration { get; set; }
    }
}
