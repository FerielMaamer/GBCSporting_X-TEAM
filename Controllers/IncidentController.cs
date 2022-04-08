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


        public IActionResult AllIncidents()
        {
            return View();  
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            var content = loadContent();
            return View("Edit", content);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var incident = context.Incidents.Find(id);
            var content = loadContent();
            content.IncidentId = incident.IncidentId;
            content.Title = incident.Title;
            content.Description = incident.Description;
            content.DateOpened = incident.DateOpened;
            content.DateClosed = incident.DateClosed;
            content.ProductId = incident.ProductId;
            content.TechnicianId = incident.TechnicianId;
            content.CustomerId = incident.CustomerId;

            return View(content);
        }

        [HttpPost]
        public IActionResult Edit(IncidentViewModel incident)
        {
            Incident model = new()
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
                if (model.IncidentId == 0)
                {
                    context.Incidents.Add(model);
                    TempData["message"] = $"<{model.Title}> Incident was Added!";
                }
                else
                {
                    context.Incidents.Update(model);
                    TempData["message"] = $"<{model.Title}> Incident was Updated!";
                }
                context.SaveChanges();
                return RedirectToAction("Incidents", "Home");
            }
            else
            {
                ViewBag.Action = (model.IncidentId == 0) ? "Add" : "Edit";
                var content = loadContent();
                content.IncidentId = incident.IncidentId;
                content.Title = incident.Title;
                content.Description = incident.Description;
                content.DateOpened = incident.DateOpened;
                content.DateClosed = incident.DateClosed;
                content.ProductId = incident.ProductId;
                content.TechnicianId = incident.TechnicianId;
                return View(content);
            }
            
        }

        [HttpGet]
        public IActionResult Delete(int id, int cid)
        {

            var incident = context.Incidents.Find(id);
            var customer = context.Customers.Find(cid);
            var vm = new IncidentViewModel
            {
                Title = incident.Title,
                firstName = customer.FirstName,
                LastName = customer.LastName,
                IncidentId = incident.IncidentId
            };

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

        public IncidentViewModel loadContent()
        {
            var vm = new IncidentViewModel();
            var customers = context.Customers;
            var products = context.Products;
            var technicians = context.Technicians;

            var customerList = customers.Select(
                c => new SelectListItem
                {
                    Text = c.FirstName + " " + c.LastName,
                    Value = c.CustomerId.ToString(),
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
            return vm;
        }
    }
}
