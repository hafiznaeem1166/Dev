using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLP_Delivery.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace NLP_Delivery.Controllers
{
    [Authorize(Roles ="Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser<int>> UserManager;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser<int>> userManager)
        {
            _logger = logger;
            UserManager = userManager;
        }

        public IActionResult Index()
        {

            //User.Identity.Get
            //ClaimTypes.NameIdentifier
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            //if (User.IsInRole("Admin"))
            //{
            //    return View("Privacy");
            //}
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
