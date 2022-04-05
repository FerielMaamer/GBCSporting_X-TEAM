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
            var incident = context.Incidents.Find(id);
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
            vm.IncidentId = incident.IncidentId;
            vm.Title = incident.Title;
            vm.Description = incident.Description;
            vm.DateOpened = incident.DateOpened;
            vm.DateClosed = incident.DateClosed;
            vm.ProductId = incident.ProductId;
            vm.TechnicianId = incident.TechnicianId;

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(IncidentViewModel incident)
        {
            Incident model = new Incident()
            {
                IncidentId = incident.IncidentId,
                ProductId = incident.ProductId,
                CustomerId = incident.CustomerId,
                Title = incident.Title,
                DateOpened = incident.DateOpened,
                DateClosed = incident.DateClosed,
                Description = incident.Description,
                TechnicianId = incident.TechnicianId
            };

            if (ModelState.IsValid)
            {
                if (incident.IncidentId == 0)
                {
                    context.Incidents.Add(model);
                    return RedirectToAction("Incidents", "Home");
                    TempData["message"] = $"<{model.Title}> Incident was Added!";
                }
                else
                {
                    context.Incidents.Update(model);
                    context.SaveChanges();
                    TempData["message"] = $"<{model.Title}> Incident was Updated!";
                    return RedirectToAction("Incidents", "Home");

                }
            }
            else
            {
                ViewBag.Action = (incident.IncidentId == 0) ? "Add" : "Edit";
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

                incident.Customers = customerList;
                incident.Products = productList;
                incident.Technicians = technicianList;
                incident.IncidentId = incident.IncidentId;
                incident.Title = incident.Title;
                incident.Description = incident.Description;
                incident.DateOpened = incident.DateOpened;
                incident.DateClosed = incident.DateClosed;
                incident.ProductId = incident.ProductId;
                incident.TechnicianId = incident.TechnicianId;

                return View(incident);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id, int cid)
        {

            var incident = context.Incidents.Find(id);
            var customer = context.Customers.Find(cid);
            var vm = new IncidentViewModel();
            vm.Title = incident.Title;
            vm.firstName = customer.FirstName;
            vm.LastName = customer.LastName;
            vm.IncidentId = incident.IncidentId;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Delete(IncidentViewModel incident)
        {

            var model = context.Incidents.Find(incident.IncidentId);


            context.Incidents.Remove(model);
            context.SaveChanges();
            TempData["message"] = $"<{model.Title}> Incident was Deleted!";
            return RedirectToAction("Incidents", "Home");
        }
    }
}
