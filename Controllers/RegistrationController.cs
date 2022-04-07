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
        public IActionResult List()        {
            ViewBag.message = null;
            ViewBag.Products = context.Products.ToList();
            var custID = Int32.Parse(Request.Form["custId"]);
            ViewBag.CustName = context.Customers.Where(x => x.CustomerId == custID).ToList();
            IQueryable<Incident> query = context.Incidents;
            query = query.Where(x => x.Customer.CustomerId == custID).Include(p => p.Product);
            ViewBag.Incidents = query.ToList();            
            var incidents = context.Incidents.Find(1); 
            if ((query != null) && (!query.Any()))
            {
                ViewBag.message = "no results to show";
                return View();
            }
            else
            {
                return View(incidents);                
            }
            
        }
    }
}
