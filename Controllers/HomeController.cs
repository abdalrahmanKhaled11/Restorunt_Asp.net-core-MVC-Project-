using Microsoft.AspNetCore.Mvc;
using Restorunt.Models;
using System.Diagnostics;

namespace Restorunt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;

        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }


        public IActionResult Services()
        {
            return View();
        }

        public IActionResult menu()
        {
            var categories=_context.Categories.ToList();

            return View(categories);
        }

        public IActionResult GetProductByCatgoryId(int id) 
        {
            var products = _context.Products.Where(product => product.CategoryId == id).ToList();
            return View(products);

        }

        public IActionResult contact()
        {
            return View();
        }

        public IActionResult Booking()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
