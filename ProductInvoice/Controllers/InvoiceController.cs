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
            Guid i;
            if (HttpContext.Session.GetString("InvoiceId") == null)
                HttpContext.Session.SetString("InvoiceId", "null");

            if(HttpContext.Session.GetString("InvoiceId").Equals("null") == true)
                {
                    //invoiceId = Guid.NewGuid();
                    HttpContext.Session.SetString("InvoiceId",Guid.NewGuid().ToString());
                i = new Guid(HttpContext.Session.GetString("InvoiceId"));
                    HttpContext.Session.SetString("TotalPrice","0");
                    _invoiceRepository.AddInvoice(i);
                }
            i = new Guid(HttpContext.Session.GetString("InvoiceId"));

            ViewBag.InvoiceId = i.ToString();
            
            
            
            ViewBag.totPrice = HttpContext.Session.GetString("TotalPrice");
            IEnumerable<Product> products = _productRepository.products();
            return View(products);
        }
        [HttpPost]
        public IActionResult AddItems(Guid productId)
        {
            Product product = _productRepository.GetProductById(productId);

            Guid InvoiceId = new Guid(HttpContext.Session.GetString("InvoiceId"));
             _invoiceItemsRepository.AddItems(InvoiceId, product);
            
           decimal total = _invoiceRepository.UpdatePrice(product, InvoiceId);
             HttpContext.Session.SetString("TotalPrice", total.ToString());
            return RedirectToAction("AddInvoice");
        }
        [HttpPost]
        public IActionResult RemoveItems(Guid productId)
        {
            Product product = _productRepository.GetProductById(productId);

            Guid InvoiceId = new Guid(HttpContext.Session.GetString("InvoiceId"));
            _invoiceItemsRepository.RemoveItems(InvoiceId, product);

            decimal total = _invoiceRepository.SubtractPrice(product, InvoiceId);
            HttpContext.Session.SetString("TotalPrice", total.ToString());

            return RedirectToAction("AddInvoice");
        }

        [HttpPost]
        public IActionResult Cancel()
        {
            Guid invoiceId = new Guid(HttpContext.Session.GetString("InvoiceId"));
            _invoiceRepository.Cancel(invoiceId);
            _invoiceItemsRepository.RemoveItems(invoiceId);
            HttpContext.Session.SetString("InvoiceId","null");
            HttpContext.Session.SetString("TotalPrice","0");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Save()
        {
            HttpContext.Session.SetString("InvoiceId", "null");
            return RedirectToAction("Index");
        }

        public IActionResult Search(string searchTerm)
        {
            Guid invoiceId = new Guid(HttpContext.Session.GetString("InvoiceId"));
            if (searchTerm != null)
            {
                IEnumerable<Product> products = _productRepository.Search(searchTerm);
                ViewBag.InvoiceId = invoiceId.ToString();
                return View("AddInvoice", products);
            }
            else
            {
                return RedirectToAction("AddInvoice");
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}