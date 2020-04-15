using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RDAT.Data;
using RDAT.Models;
using RDAT.ViewModels;

namespace RDAT.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            DashboardViewModel _model = new DashboardViewModel();

            using RDATContext context = new RDATContext();

            _model.BadgeTotalActiveDrivers = context.Drivers.Count();
            _model.BadgeTotalActiveCompanies = 77; // context.Companys.Where(d => ??).Count();
            _model.BadgeOutstandingDrugTest = 99;
            _model.BadgeOutstandingAlcoholTest = 88;

            _model.FavoriteCompanies = context.Companys.Where(c => c.isFavorite).OrderByDescending(p => p.Id).ToList();
            
            _model.FavoriteDrivers = context.Drivers.Where(d => d.isFavorite).OrderByDescending(p => p.Id).ToList();

            return View(_model);
        }

        public IActionResult Blank()
        {
            return View();
        }

        public IActionResult Search(DashboardViewModel model)
        {
            var _searchTerm = model.SearchDrivers;


            return View(model);
        }

        public IActionResult MyViewComponent(string searchTerm)
        {
            return ViewComponent("FeaturedDrivers", new { searchTerm = searchTerm });
        }


        public IActionResult Dashboard()
        {
            DashboardViewModel _model = new DashboardViewModel();

            using RDATContext context = new RDATContext();

            _model.BadgeTotalActiveDrivers = context.Drivers.Count();
            _model.BadgeTotalActiveCompanies = 77; // context.Companys.Where(d => ??).Count();
            _model.BadgeOutstandingDrugTest = 99;
            _model.BadgeOutstandingAlcoholTest = 88;

            var companys = context.Companys.OrderByDescending(p => p.Id).Take(5);

            return View(_model);
        }
        public IActionResult Tables()
        {
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

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }

        public IActionResult LoginExample()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LoginExample(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
            }

            var grandmaClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim(ClaimTypes.Email, "Bob@fmail.com"),
            };

            var licenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Bob K Foo"),
                new Claim("DrivingLicense", "A+"),
            };

            var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "Grandma Identity");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "Government");

            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity, licenseIdentity });
            //-----------------------------------------------------------
            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }
    }
}
