using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductInvoice.Models;
using ProductInvoice.Services;

namespace ProductInvoice.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;

        public ProductController(IProductRepository productRepository, ILogger logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Product> products = _productRepository.products(); 
            return View(products);
        }
        [HttpGet]
        public IActionResult GetProductById(Guid ProductId)
        {
            Product product = _productRepository.GetProductById(ProductId);
            return View(product);
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            List<SelectListItem> ProductCategory = new List<SelectListItem>()
            {
                new SelectListItem{ Text= "Mobiles", Value="Mobiles" },
                new SelectListItem{Text= "Television",Value="Television" },
                new SelectListItem{Text= "Air Conditioner",Value="Air Conditioner" },
                new SelectListItem{Text= "Laptop",Value="Laptop" },
                new SelectListItem{Text ="Washing Machine",Value="Washing Machine" }
            };

            ViewBag.ProductCategory = ProductCategory;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct([Bind("ProductId,ProductName,ProductCategory,ProductBasePrice,ProductCreated,ProductUpdated")] Product product)
        {
            product.ProductId = Guid.NewGuid();
            product.ProductCreated = DateTime.Now;
            product.ProductUpdated = DateTime.Now;
            
            if (product.ProductName != null && product.ProductBasePrice!=null)
            {
                _productRepository.AddProduct(product);
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Index", _productRepository.products().ToList()) });
            }
            else
            {
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddProduct", product) });
            }
        }

        [HttpGet]
        public IActionResult UpdateProduct(Guid ProductId)
        {

            List<SelectListItem> ProductCategory = new List<SelectListItem>()
            {
                new SelectListItem{ Text= "Mobiles", Value="Mobiles" },
                new SelectListItem{Text= "Television",Value="Television" },
                new SelectListItem{Text= "Air Conditioner",Value="Air Conditioner" },
                new SelectListItem{Text= "Laptop",Value="Laptop" },
                new SelectListItem{Text ="Washing Machine",Value="Washing Machine" }
            };

            ViewBag.ProductCategory = ProductCategory;
           
            Product product = _productRepository.GetProductById(ProductId);
            return View(product);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Guid ProductId,Product product)
        {
            _productRepository.UpdateProduct(ProductId,product);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteProduct(Guid ProductId)
        {
            _productRepository.DeleteProduct(ProductId);
            return RedirectToAction("Index");
        }

    }
}
