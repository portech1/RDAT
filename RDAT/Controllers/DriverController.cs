using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDAT.Data;
using RDAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Controllers
{
    [Authorize]
    public class DriverController : Controller
    {
        public IActionResult Index(int id = 0)
        {
            using RDATContext context = new RDATContext();

            var drivers = new List<Driver>();
            var listName = "All Drivers";
            

            if(id != 0)
            {
                drivers = context.Drivers.Where(c => c.Company_id == id).ToList();
                listName = context.Companys.Where(c => c.Id == id).FirstOrDefault().Name + " Drivers";
            }
            else
            {
drivers = context.Drivers.ToList();
            }

            var count = drivers.Count();
            ViewBag.ListName = listName;

            return View(drivers);
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

        public IActionResult Edit(int id)
        {
            using RDATContext context = new RDATContext();

            Driver _driver = context.Drivers.Where(d => d.Id == id).FirstOrDefault();
            ViewBag.CompanyName = _driver.DriverName;

            // return data;
            return View(_driver);
        }

        public ActionResult Favorite(int id)
        {
            var thisID = id;

            using RDATContext context = new RDATContext();

            Driver _driver = context.Drivers.Where(c => c.Id == id).FirstOrDefault();

            _driver.isFavorite = !_driver.isFavorite;

            context.Update(_driver);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                using RDATContext context = new RDATContext();

                Driver _driver = context.Drivers.Where(d => d.Id == id).FirstOrDefault();

                _driver.DriverName = collection["DriverName"];
                _driver.Phone = collection["Phone"];
                _driver.Fax = collection["Fax"];
                _driver.AddressLine1 = collection["AddressLine1"];
                _driver.AddressLine2 = collection["AddressLine2"];
                _driver.City = collection["City"];
                _driver.State = collection["State"];
                _driver.Zipcode = collection["Zipcode"];
                _driver.Email = collection["Email"];
                _driver.CDL = collection["CDL"];
                _driver.Cell = collection["Cell"];
                
                if(Int32.TryParse(collection["Company_id"], out int result)) {
                    _driver.Company_id = Int32.Parse(collection["Company_id"]);
                };
                
                _driver.EnrollmentDate = DateTime.Parse(collection["EnrollmentDate"]);
                _driver.TerminationDate = DateTime.Parse(collection["TerminationDate"]);

                context.Update(_driver);
                context.SaveChanges();

                // return data;
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }
    }
}