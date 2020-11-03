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
using Microsoft.Extensions.Configuration.UserSecrets;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            DashboardViewModel _model = new DashboardViewModel();

            using RDATContext context = new RDATContext();
            List<TestingLog> activeLogs = context.TestingLogs.Where(tl => Convert.ToInt32(tl.Reported_Results) < 1).ToList();

            char[] goodResults = { '1', '2', '3' };

            // Get User Roles
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            bool IsAdmin = currentUser.IsInRole("Admin");
            bool IsReadOnly = currentUser.IsInRole("ReadOnly");
            
            _model.BadgeTotalActiveDrivers = context.Drivers.Where(d => d.TerminationDate == null && !d.isDelete).Count();
            _model.BadgeTotalActiveCompanies = context.Companys.Where(c => c.Status == "1" && c.isDelete != true).Count(); // context.Companys.Where(d => ??).Count();
            _model.BadgeOutstandingDrugTest = activeLogs.Where(tl => tl.Test_Type == "Drug").Count();
            _model.BadgeOutstandingAlcoholTest = activeLogs.Where(tl => tl.Test_Type == "Alcohol").Count();

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

        public IActionResult SearchCompanies(DashboardViewModel model)
        {
            var _searchTerm = model.SearchCompanies;

            return View(model);
        }

        public IActionResult GetFeaturedDrivers(string searchTerm)
        {
            return ViewComponent("FeaturedDrivers", new { searchTerm = searchTerm });
        }

        public IActionResult GetFeaturedCompanies(string searchTerm)
        {
            return ViewComponent("FeaturedCompanies", new { searchTerm = searchTerm });
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

        //private async void ResetUserPassword(string userId, string password)
        //{
            //string userID = (string)u.Id.ToString();
            //// Reset password
            //var user = await UserManager<ApplicationUser>.FindByIdAsync(userId);

            //var token = await UserManager<ApplicationUser>.GeneratePasswordResetTokenAsync(user);

            //var result = await UserManager<ApplicationUser>.ResetPasswordAsync(user, token, password);

            //ApplicationUser user = await AppUserManager.FindByIdAsync(userId);
            //if (user == null)
            //{
            //    return NotFound();
            //}
            //user.PasswordHash = AppUserManager.PasswordHasher.HashPassword(password);
            //var result = await AppUserManager.UpdateAsync(user);
            //if (!result.Succeeded)
            //{
            //    //throw exception......
            //}
            //return Ok();

            //string resetToken = await UserManager<ApplicationUser>.GeneratePasswordResetTokenAsync(userId);
            //IdentityResult passwordChangeResult = await UserManager<ApplicationUser>.ResetPasswordAsync(userId, resetToken, password);

        //    return true;
        //}


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
