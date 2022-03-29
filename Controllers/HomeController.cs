using GBCSporting_X_TEAM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GBCSporting_X_TEAM.Controllers
{
    public class HomeController : Controller
    {
        private GbcSportingContext context { get; set; }

        public HomeController(GbcSportingContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Products()
        {
            var products= context.Products.OrderByDescending(x => x.ReleaseDate).ToList();
            return View(products);
        }

        public IActionResult Technicians()
        {
            var technician = context.Technicians.OrderBy(x => x.Name).ToList();
            return View(technician);
        }

        public IActionResult Customers()
        {
            var customer = context.Customers.OrderBy(x => x.FirstName).ToList();
            return View(customer);
        }

        public IActionResult Incidents()
        {
            var incident = context.Incidents.Include(c => c.Customer).Include(p => p.Product).OrderBy(x => x.Title).ToList();
            return View(incident);
        }

        public IActionResult Registrations()
        {
            return View();
        }

        public IActionResult UpdateIncident()
        {
            ViewBag.Technician = context.Technicians.OrderBy(x => x.Name).ToList();
            return View();           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}