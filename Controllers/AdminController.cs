using Microsoft.AspNetCore.Mvc;
using Restorunt.Models;

namespace Restorunt.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;

        public AdminController(ModelContext context) 
        {

            _context = context;


        }
        public IActionResult Index()
        {
            ViewData["adminName"] = HttpContext.Session.GetString("AdminName");


            ViewBag.CustomerCount = _context.Customers.Count();
            ViewBag.TotalSales = _context.Products.Sum(product=> product.Sale);
            ViewBag.MaxPrice = _context.Products.Max(product => product.Price);
            ViewBag.MinPrice = _context.Products.Min(product => product.Price);




            return View();
        }
    }
}
