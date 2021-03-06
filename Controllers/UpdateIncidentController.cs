using GBCSporting_X_TEAM.Models;
using GBCSporting_X_TEAM.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        
        public IActionResult Index(int id)
        {

           

            var content = context.Incidents.Where(x => x.TechnicianId == id).
                Include(c => c.Customer).
                Include(p => p.Product).
                Include(t => t.Technician)
                .Select(i => new IncidentViewModel
                {
                    IncidentId = i.IncidentId,
                    ProductId = i.ProductId,
                    TechnicianId = i.TechnicianId,
                    CustomerId = i.CustomerId,
                    Title = i.Title,
                    firstName = i.Customer.FirstName,
                    LastName = i.Customer.LastName,
                    ProductName = i.Product.Name,
                    DateOpened = i.DateOpened,


                });
            ViewBag.Name = context.Technicians.Find(id).Name;


            return View(content);

        }

        [HttpPost]
        public IActionResult Index()
        {  
          
            var TechId = Int32.Parse(Request.Form["techId"]);
            
            var content = context.Incidents.Where(x => x.TechnicianId == TechId).
                Include(c => c.Customer).
                Include(p => p.Product).
                Include(t => t.Technician)
                .Select(i => new IncidentViewModel
                {
                         IncidentId = i.IncidentId,
                         ProductId = i.ProductId,
                         TechnicianId = i.TechnicianId,
                         CustomerId = i.CustomerId,
                         Title = i.Title,
                         firstName = i.Customer.FirstName,
                         LastName = i.Customer.LastName,
                        ProductName = i.Product.Name,
                        DateOpened = i.DateOpened,
                        

                });
                    ViewBag.Name  = context.Technicians.Find(TechId).Name;


            return View(content);

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Update";
            var incident = context.Incidents.Find(id);

            IncidentViewModel content = new()
            {
                TechnicianId= incident.TechnicianId,
                IncidentId = incident.IncidentId,
                Title = incident.Title,
                Description = incident.Description,
                DateOpened = incident.DateOpened,
                DateClosed = incident.DateClosed,
                ProductId = incident.ProductId,
                CustomerId = incident.CustomerId,


                TechnicianName = context.Technicians.Find(incident.TechnicianId).Name,
                firstName = context.Customers.Find(incident.CustomerId).FirstName,
                LastName = context.Customers.Find(incident.CustomerId).LastName,
                ProductName = context.Products.Find(incident.ProductId).Name,

               

        };
           

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
                TechnicianId = incident.TechnicianId,
               
                
            };

            if (ModelState.IsValid)
            {
                if (model.IncidentId == 0)
                    context.Incidents.Add(model);
                else
                    context.Incidents.Update(model);
                context.SaveChanges();

                return RedirectToAction("Index", new { id = model.TechnicianId });
            }
            else
            {
                ViewBag.Action = (model.IncidentId == 0) ? "Add" : "Edit";
                incident.TechnicianName = context.Technicians.Find(model.TechnicianId).Name;
                incident.firstName = context.Customers.Find(model.CustomerId).FirstName;
                incident.LastName = context.Customers.Find(model.CustomerId).LastName;
                incident.ProductName = context.Products.Find(model.ProductId).Name;
                return View(incident);
            }
        }

       
    }
}
