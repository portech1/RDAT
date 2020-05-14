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
using RDAT.ViewModels;
using Newtonsoft.Json.Linq;

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


            if (id != 0)
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

        public UpdateDriverResults GetDriver(int id)
        {
            var thisID = id;

            using RDATContext context = new RDATContext();
            UpdateDriverResults _driverResults = new UpdateDriverResults();

            Driver _driver = context.Drivers.Where(c => c.Id == id).FirstOrDefault();
            int _batchId = context.Batches.OrderByDescending(b => b.Id).FirstOrDefault().Id;

            // Get TestingLog
            TestingLog _logDrug = context.TestingLogs.Where(l => l.Driver_Id == id && l.Test_Type == "Drug" && l.Batch_Id == _batchId).FirstOrDefault();

            TestingLog _logAlcohol = context.TestingLogs.Where(l => l.Driver_Id == id && l.Test_Type == "Alcohol" && l.Batch_Id == _batchId).FirstOrDefault();

            // Set Values
            _driverResults.Driver_Id = _driver.Id;
            _driverResults.Driver_Name = _driver.DriverName;
            _driverResults.Company_Name = context.Companys.Where(c => c.Id == _driver.Company_id).FirstOrDefault().Name;
            _driverResults.Driver_UniqueID = _driver.UniqueId.ToString();

            // Alcohol Test
            if (_logAlcohol != null)
            {
                _driverResults.Alcohol_Show = true;
                _driverResults.Alcohol_TestingLogID = _logAlcohol.Id;
                _driverResults.Alcohol_Test_Date = _logAlcohol.TestDate;
                _driverResults.Alcohol_Specimen_Id = _logAlcohol.Specimen_Id.ToString();
                _driverResults.Alcohol_Results_Date = _logAlcohol.ResultsDate;
                _driverResults.Alcohol_Reported_Result = _logAlcohol.Reported_Results;
            }
            else
            {
                _driverResults.Alcohol_Show = false;
            }

            // Drug Test
            if (_logDrug != null)
            {
                _driverResults.Drug_Show = true;
                _driverResults.Drug_TestingLogID = _logDrug.Id;
                _driverResults.Drug_Test_Date = _logDrug.TestDate;
                _driverResults.Drug_Specimen_Id = _logDrug.Specimen_Id.ToString();
                _driverResults.Drug_Results_Date = _logDrug.ResultsDate;
                _driverResults.Drug_Reported_Result = _logDrug.Reported_Results;
            }
            else
            {
                _driverResults.Drug_Show = false;
            }

            return _driverResults;
        }

        [HttpPost]
        public Driver UpdateDriver([FromBody] UpdateDriverResults postdata)
        {
            // var thisID = id;
            // JObject json = JObject.Parse(obj);

            using RDATContext context = new RDATContext();

            Driver _driver = context.Drivers.Where(c => c.Id == 1).FirstOrDefault();
            
            return _driver;
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

                if (Int32.TryParse(collection["Company_id"], out int result))
                {
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