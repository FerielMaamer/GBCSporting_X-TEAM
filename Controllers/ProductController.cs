using GBCSporting_X_TEAM.Models;
using Microsoft.AspNetCore.Mvc;

namespace GBCSporting_X_TEAM.Controllers
{
    public class ProductController : Controller
    {
        private GbcSportingContext context { get; set; }

        public ProductController(GbcSportingContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Product());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var product = context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductId == 0)
                {
                    context.Products.Add(product);
                    TempData["message"] = $"{product.Name} was added!";
                }
                else
                {
                    context.Products.Update(product);
                    context.SaveChanges();
                    TempData["message"] = $"{product.Name} was updated!";
                }
                return RedirectToAction("Products", "Home");
            }
            else
            {
                ViewBag.Action = (product.ProductId == 0) ? "Add" : "Edit";
                return View(product);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
            TempData["message"] = $"model was deleted!";
            return RedirectToAction("Products", "Home");
        }
    }
}
