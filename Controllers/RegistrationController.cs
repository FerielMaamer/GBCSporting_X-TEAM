using Microsoft.AspNetCore.Mvc;
using GBCSporting_X_TEAM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

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
            int custID = Int32.Parse(Request.Form["custId"]);
            HttpContext.Session.SetInt32("custID", custID);
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
                if (context.Registrations.Find(1,registration.ProductId) == null)
                {
                    context.Registrations.Add(registration);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Registration");
                }
                
                return RedirectToAction("Index", "Registration");
            
        }


    }
}