using Microsoft.EntityFrameworkCore;

namespace ProductInvoice.Models
{
    public static class ModelBuilderExtensions
    {

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId= (Guid)Guid.NewGuid(),
                    ProductName = "Novice",
                    ProductCategory = "Mobiles",
                    ProductBasePrice = 9999,
                    ProductCreated = DateTime.Now,
                    ProductUpdated = DateTime.Now,
                },
                new Product
                {
                    ProductId = (Guid)Guid.NewGuid(),
                    ProductName = "Stans Pro",
                    ProductCategory = "Television",
                    ProductBasePrice = 49999,
                    ProductCreated = DateTime.Now,
                    ProductUpdated = DateTime.Now,
                },
                new Product
                {
                    ProductId = (Guid)Guid.NewGuid(),
                    ProductName = "Ultra Washing",
                    ProductCategory = "Washing Machine",
                    ProductBasePrice = 19999,
                    ProductCreated = DateTime.Now,
                    ProductUpdated = DateTime.Now,
                },
                new Product
                {
                    ProductId = (Guid)Guid.NewGuid(),
                    ProductName = "Cool Breeze",
                    ProductCategory = "Air Conditioner",
                    ProductBasePrice = 59999,
                    ProductCreated = DateTime.Now,
                    ProductUpdated = DateTime.Now,
                }, new Product
                {
                    ProductId = (Guid)Guid.NewGuid(),
                    ProductName = "T14",
                    ProductCategory = "Laptop",
                    ProductBasePrice = 99999,
                    ProductCreated = DateTime.Now,
                    ProductUpdated = DateTime.Now,
                }

                ); 


        }
    }
}
