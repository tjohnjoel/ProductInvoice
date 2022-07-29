using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductInvoice.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
       
        [DisplayName("Product Category")]
        public string ProductCategory { get; set; }
        
        [DisplayName("Product Base Price")]
        public decimal ProductBasePrice { get; set; }
        [DisplayName("Created Date")]
        public DateTime ProductCreated { get; set; }
        [DisplayName("Last Modified Date")]
        public DateTime ProductUpdated { get; set; }

        public List<Discount> discounts { get; set; }
        public List<InvoiceItems> invoiceItems { get; set; }
    }
}
