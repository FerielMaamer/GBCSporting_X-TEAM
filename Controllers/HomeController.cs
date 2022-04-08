using GBCSporting_X_TEAM.Models;
using GBCSporting_X_TEAM.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult About()
        {
            return View(About);
        }

        public IActionResult Products()
        {
            var products = context.Products.OrderByDescending(x => x.ReleaseDate).ToList();
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
            var viewModels = context.Incidents.
                    Include(i => i.Customer).
                    Include(i => i.Product).
                    Select(i => new IncidentViewModel
                    {
                        Title = i.Title,
                        firstName = i.Customer.FirstName,
                        LastName = i.Customer.LastName,
                        ProductName = i.Product.Name,
                        DateOpened = i.DateOpened
                    });

            return View(viewModels);
        }




        public IActionResult Registrations()
        {
            ViewBag.Customer = context.Customers.OrderBy(x => x.CustomerId).ToList();

            return View();
        }

        public IActionResult UpdateIncident()
        {
            var vm = new IncidentViewModel();
          
            var technicians = context.Technicians;
           
            var technicianList = technicians.Select(
                c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.TechnicianId.ToString()
                }).ToList();

          
            vm.Technicians = technicianList;
            
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}