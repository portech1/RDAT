using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDAT.Data;
using RDAT.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Controllers
{
    [Authorize]
    public class DriverController : Controller
    {
        public IActionResult Index()
        {
            using RDATContext context = new RDATContext();

            var drivers = context.Drivers;
            //var _driverList = [];

            //foreach(driver d in drivers)
            //{
            //    _driverList.Add(d)
            //}

            var count = drivers.Count();

            return View(drivers.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
    [Bind("DriverName,SSN,AddressLine1,AddressLine2,City,State,Location,UniqueId,Phone,Fax,Cell,Email,EnrollmentDate,TerminationDate,CDL")] Driver driver)
        {
            using RDATContext context = new RDATContext();
            var drivers = context.Drivers;
            try
            {
                if (ModelState.IsValid)
                {
                    drivers.Add(driver);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(driver);
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}