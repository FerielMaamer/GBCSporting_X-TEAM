﻿using GBCSporting_X_TEAM.Models;
using GBCSporting_X_TEAM.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GBCSporting_X_TEAM.Controllers
{
    public class HomeController : Controller
    {
        private GbcSportingContext context { get; set; }

        public HomeController(GbcSportingContext ctx)
        {
            context = ctx;
        }
        [Route("Home")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("Products")]
        public IActionResult Products()
        {
            var products= context.Products.OrderByDescending(x => x.ReleaseDate).ToList();
            return View(products);
        }
        [Route("Technicians")]
        public IActionResult Technicians()
        {
            var technician = context.Technicians.OrderBy(x => x.Name).ToList();
            return View(technician);
        }
        [Route("Customers")]
        public IActionResult Customers()
        {
            var customer = context.Customers.OrderBy(x => x.FirstName).ToList();
            return View(customer);
        }
        [Route("Incidents")]
        public IActionResult Incidents()
        {
            var viewModels = context.Incidents.
                    Include(i => i.Customer).
                    Include(i => i.Product).
                    Select(i => new IncidentViewModel
                    {
                        IncidentId = i.IncidentId,
                        ProductId = i.ProductId,
                        CustomerId = i.CustomerId,
                        Title = i.Title,
                        firstName = i.Customer.FirstName,
                        LastName = i.Customer.LastName,
                        ProductName = i.Product.Name,
                        DateOpened = i.DateOpened
                    });

            return View(viewModels);
        
        }
        [Route("Registrations")]
        public IActionResult Registrations()
        {
            ViewBag.Customer = context.Customers.OrderBy(x => x.CustomerId).ToList();
            return View();
        }

        public IActionResult UpdateIncident()
        {
            ViewBag.Technician = context.Technicians.OrderBy(x => x.TechnicianId).ToList();
            return View();           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}