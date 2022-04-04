using Microsoft.AspNetCore.Mvc;
using GBCSporting_X_TEAM.Models;
using System.Linq;
using GBCSporting_X_TEAM.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GBCSporting_X_TEAM.Controllers
{
    public class IncidentController : Controller
    {
        private GbcSportingContext context { get; set; }

        public IncidentController(GbcSportingContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            var vm = new IncidentViewModel();
            var customers = context.Customers;
            var products = context.Products;
            var technicians = context.Technicians;

            var customerList = customers.Select(
                c => new SelectListItem
            {
               Text = c.FirstName + " " + c.LastName,
               Value = c.CountryId.ToString(),
            }).ToList();

            var productList = products.Select(
                c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.ProductId.ToString()
                }).ToList();

            var technicianList = technicians.Select(
                c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.TechnicianId.ToString()
                }).ToList();

            vm.Customers = customerList;
            vm.Products = productList;
            vm.Technicians = technicianList;


            return View("Edit", vm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var vm = new IncidentViewModel();
            var customers = context.Customers;
            var products = context.Products;
            var technicians = context.Technicians;

            var customerList = customers.Select(
                c => new SelectListItem
                {
                    Text = c.FirstName + " " + c.LastName,
                    Value = c.CountryId.ToString(),
                }).ToList();

            var productList = products.Select(
                c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.ProductId.ToString()
                }).ToList();

            var technicianList = technicians.Select(
                c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.TechnicianId.ToString()
                }).ToList();

            vm.Customers = customerList;
            vm.Products = productList;
            vm.Technicians = technicianList;

            Incident incident = context.Incidents.Find(id);

            return View(vm);
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

        [HttpGet]
        public IActionResult Delete(int id, int cid)
        {
            ViewBag.Customer = context.Customers.Find(cid);
            var incident = context.Incidents.Find(id);
            return View(incident);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            context.Incidents.Remove(incident);
            context.SaveChanges();
            return RedirectToAction("Incidents", "Home");
        }
    }
}
