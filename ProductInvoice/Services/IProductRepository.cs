using ProductInvoice.Models;

namespace ProductInvoice.Services
{
    public interface IProductRepository
    {
        IEnumerable<Product> products();
        void UpdateProduct(Guid ProductId, Product product);
        void AddProduct(Product product);
        Product GetProductById(Guid ProductId);
        void DeleteProduct(Guid ProductId);
        IEnumerable<Product> Search(string searchTerm);
    }
}
