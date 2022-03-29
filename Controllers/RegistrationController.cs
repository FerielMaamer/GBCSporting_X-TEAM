using Microsoft.AspNetCore.Mvc;
using GBCSporting_X_TEAM.Models;

namespace GBCSporting_X_TEAM.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
