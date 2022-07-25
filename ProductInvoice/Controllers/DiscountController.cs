using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductInvoice.Models;
using ProductInvoice.Services;

namespace ProductInvoice.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;

        public DiscountController(IDiscountRepository discountRepository, ILogger logger, IProductRepository productRepository)
        {
            _discountRepository = discountRepository;
            _logger = logger;
            _productRepository = productRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Discount> discounts = _discountRepository.discounts();
            
            return View(discounts);
        }
        [HttpGet]
        public IActionResult AddDiscount()
        {
            List<Product> products = _productRepository.products().ToList();
            ViewBag.Products = products;
            return View();
        }
        [HttpPost]
        public IActionResult AddDiscount(Guid ProductId,Discount discount)
        {
            discount.ProductId = ProductId;
            _discountRepository.AddDiscount(discount);
            return RedirectToAction("Index");
        }
    }
}
