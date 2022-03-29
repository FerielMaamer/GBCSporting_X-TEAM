using GBCSporting_X_TEAM.Models;
using Microsoft.AspNetCore.Mvc;

namespace GBCSporting_X_TEAM.Controllers
{
    public class TechnicianController : Controller
    {
        private GbcSportingContext context { get; set; }

        public TechnicianController(GbcSportingContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Technician());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var technician = context.Technicians.Find(id);
            return View(technician);
        }

        [HttpPost]
        public IActionResult Edit(Technician technician)
        {
            if (ModelState.IsValid)
            {
                if (technician.TechnicianId == 0)
                    context.Technicians.Add(technician);
                else
                    context.Technicians.Update(technician);
                context.SaveChanges();
                return RedirectToAction("Technicians", "Home");
            }
            else
            {
                ViewBag.Action = (technician.TechnicianId == 0) ? "Add" : "Edit";
                return View(technician);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var technician = context.Technicians.Find(id);
            return View(technician);
        }

        [HttpPost]
        public IActionResult Delete(Technician technician)
        {
            context.Technicians.Remove(technician);
            context.SaveChanges();
            return RedirectToAction("Technicians", "Home");
        }


    }
}
