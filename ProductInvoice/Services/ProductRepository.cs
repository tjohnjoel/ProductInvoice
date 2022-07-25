using Microsoft.EntityFrameworkCore;
using ProductInvoice.Models;

namespace ProductInvoice.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            product.ProductId = Guid.NewGuid();
            product.ProductCreated = DateTime.Now;
            product.ProductUpdated = DateTime.Now;

            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(Guid ProductId)
        {
            _context.Products.Remove(GetProductById(ProductId));
            _context.SaveChanges();
        }

        public Product GetProductById(Guid ProductId)
        {
            Product product =_context.Products.Where(products => products.ProductId.CompareTo(ProductId) == 0).FirstOrDefault();
            return product;
        }

        public IEnumerable<Product> products()
        {
           return _context.Products.Include(p => p.discounts);
        }

        public void UpdateProduct(Guid ProductId, Product product)
        {
           

            Product Updatedproduct = GetProductById(ProductId);
            Updatedproduct.ProductName= product.ProductName;
            Updatedproduct.ProductCategory = product.ProductCategory;
            Updatedproduct.ProductBasePrice = product.ProductBasePrice;
            Updatedproduct.ProductUpdated = DateTime.Now;
            _context.SaveChanges();
        }
    }
}
