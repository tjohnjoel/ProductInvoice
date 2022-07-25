using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Emit;
using ProductInvoice.Models;
using ProductInvoice.Services;
using System.Diagnostics;
using System.Net;
using System.Web;
namespace ProductInvoice.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ILogger<InvoiceController> _logger;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IProductRepository _productRepository;
        private readonly IInvoiceItemsRepository _invoiceItemsRepository;
        public static Guid invoiceId { get; set; }
        public static decimal totalPrice { get; set; }
        
        public InvoiceController(IProductRepository productRepository,IInvoiceRepository invoiceRepository
            ,ILogger<InvoiceController> logger,IInvoiceItemsRepository invoiceItemsRepository)
        {
            _invoiceItemsRepository= invoiceItemsRepository;
            _productRepository = productRepository;
            _invoiceRepository = invoiceRepository;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            
            return View(_invoiceRepository.invoices());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddInvoice()
        {
            
                if (invoiceId == Guid.Empty)
                {
                    invoiceId = Guid.NewGuid();

                    _invoiceRepository.AddInvoice(invoiceId);
                }
            ViewBag.InvoiceId = invoiceId.ToString();
            ViewBag.totPrice = totalPrice;
            IEnumerable<Product> products = _productRepository.products();
            return View(products);
        }
        [HttpPost]
        public IActionResult AddItems(Guid productId)
        {
            Product product = _productRepository.GetProductById(productId);

            Guid InvoiceId = invoiceId;
             _invoiceItemsRepository.AddItems(InvoiceId, product);
            
           decimal total = _invoiceRepository.UpdatePrice(product, invoiceId);
            totalPrice = total;
            return RedirectToAction("AddInvoice");
        }
        [HttpPost]
        public IActionResult RemoveItems(InvoiceItems items)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Cancel()
        {
            _invoiceRepository.Cancel(invoiceId);
            _invoiceItemsRepository.RemoveItems(invoiceId);
            invoiceId = Guid.Empty;
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}