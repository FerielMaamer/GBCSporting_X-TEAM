using Microsoft.AspNetCore.Mvc;
using GBCSporting_X_TEAM.Models;


namespace GBCSporting_X_TEAM.Controllers
{
    public class CustomerController : Controller
    {
        private GbcSportingContext context { get; set; }

        public CustomerController(GbcSportingContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Countries = context.Countries
                .OrderBy(x => x.CountryName).ToList(); 
            return View("Edit", new Customer());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Countries = context.Countries
                .OrderBy(x => x.CountryName).ToList();
            var customer = context.Customers.Find(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.CustomerId == 0)
                    context.Customers.Add(customer);
                else
                    context.Customers.Update(customer);
                context.SaveChanges();
                return RedirectToAction("Customers", "Home");
            }
            else
            {
                ViewBag.Action = (customer.CustomerId == 0) ? "Add" : "Edit";
                ViewBag.Countries = context.Countries
                .OrderBy(x => x.CountryName).ToList();
                return View(customer);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            //ViewBag.country = context.Countries.Find(); for later
            var customer = context.Customers.Find(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            context.Customers.Remove(customer);
            context.SaveChanges();
            return RedirectToAction("Customers", "Home");
        }
    }
}
