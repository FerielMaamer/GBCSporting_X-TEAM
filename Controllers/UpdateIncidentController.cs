using GBCSporting_X_TEAM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GBCSporting_X_TEAM.Controllers
{
    public class UpdateIncidentController : Controller
    {
        private GbcSportingContext context { get; set; }

        public UpdateIncidentController(GbcSportingContext ctx)
        {
            context = ctx;
        }

        [HttpPost]
        public IActionResult Index()
        {
            ViewBag.message = null;
            var TechID = Int32.Parse(Request.Form["techId"]);
            IQueryable<Incident> query = context.Incidents;
            query = query.Where(x => x.Technician.TechnicianId == TechID)
                .Include(c => c.Customer).Include(p => p.Product).Include(t => t.Technician);
            var incidents = query.ToList();
            if ((incidents != null) && (!incidents.Any()))
            {
                ViewBag.message = "no results to show";
                return View();
            }
            else
            {
                return View(incidents);
                
                
            }

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.id = id;
            ViewBag.Action = "Edit";
            var query = context.Incidents.Where(x => x.IncidentId == id);
            ViewBag.incidents = query.ToList();
            var incident = context.Incidents.Find(id);
            return View(incident);
        }

        [HttpPost]
        public IActionResult Edit(Incident incident)
        {
            if (ModelState.IsValid)
            {
                if (incident.IncidentId == 0)
                    context.Incidents.Add(incident);
                else
                    context.Incidents.Update(incident);
                context.SaveChanges();
                return RedirectToAction("Incidents", "Home");
            }
            else
            {
                ViewBag.Action = (incident.IncidentId == 0) ? "Add" : "Edit";
                ViewBag.Customers = context.Customers.OrderBy(c => c.FirstName).ToList();
                ViewBag.Products = context.Products;
                ViewBag.Technicians = context.Technicians;
                return View(incident);
            }
        }
    }
}
