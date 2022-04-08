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
            TempData["custID"] = custID;
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

        [HttpGet]
        public IActionResult Index(int id)
        {
            ViewBag.message = null;
            ViewBag.Products = context.Products.ToList();
            var custID = HttpContext.Session.GetInt32("custID");
            ViewBag.CustName = context.Customers.Where(x => x.CustomerId == custID).ToList();
            IQueryable<Registration> query = context.Registrations;
            query = query.Where(x => x.CustomerId == custID).Include(p => p.Product);
            ViewBag.Registrations = query.ToList();
            var registration = context.Registrations.Find(custID,1);
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
            
            var id = TempData.Peek("custID");
            //registration.CustomerId = Int32.Parse(id);

               Registration model = new Registration()
                {
                    CustomerId = registration.CustomerId,
                    ProductId = registration.ProductId,
                };
                if (context.Registrations.Find(id,registration.ProductId) == null)
                {
                    context.Registrations.Add(registration);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Registration", new { id = id });
                }
                
                return RedirectToAction("Index", "Registration", new { id = id });
            
        }

        [HttpGet]
        public IActionResult Delete(int pid, int cid)
        {
            var registration = context.Registrations.Find(cid, pid);
            context.Registrations.Remove(registration);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}