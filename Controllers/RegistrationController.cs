using Microsoft.AspNetCore.Mvc;
using GBCSporting_X_TEAM.Models;
using Microsoft.EntityFrameworkCore;

namespace GBCSporting_X_TEAM.Controllers
{
    public class RegistrationController : Controller
    {
        private GbcSportingContext context { get; set; }

        public RegistrationController(GbcSportingContext ctx)
        {
            context = ctx;
        }

        [HttpPost]
        public IActionResult Index()
        {
            ViewBag.message = null;
            ViewBag.Products = context.Products.ToList();
            var custID = Int32.Parse(Request.Form["custId"]);
            ViewBag.CustName = context.Customers.Where(x => x.CustomerId == custID).ToList();
            IQueryable<Registration> query = context.Registrations;
            query = query.Where(x => x.CustomerId == custID).Include(p => p.Product);
            ViewBag.Registrations = query.ToList();
            var registration = context.Registrations.Find(1,2);
            if ((query != null) && (!query.Any()))
            {
                ViewBag.message = "no results to show";
                return View();
            }
            else
            {
                return View(registration);
            }

        }



        [HttpPost]
        public IActionResult Register(Registration registration)
        {
                registration.CustomerId = 1;
                Registration model = new Registration()
                {
                    CustomerId = registration.CustomerId,
                    ProductId = registration.ProductId,
                };
                context.Registrations.Update(registration);
                context.SaveChanges();
                return RedirectToAction("Index", "Registration");
            
        }


    }
}