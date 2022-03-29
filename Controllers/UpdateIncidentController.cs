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
        public IActionResult Index(Technician technician)
        {
            ViewBag.ID = Request.Form["techId"];
            var incidents = context.Incidents.Where(x => x.TechnicianId == technician.TechnicianId)
                .Include(c => c.Customer).Include(p => p.Product).Include(t => t.Technician).ToList();
            return View(incidents);
        }
    }
}
