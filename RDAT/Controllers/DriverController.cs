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
using System.Web.WebPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RDAT.Controllers
{
    [Authorize]
    public class DriverController : Controller
    {

        public IActionResult Index(int id = 0)
        {
            using RDATContext context = new RDATContext();
            DriverSearchModel _model = new DriverSearchModel();

            var drivers = new List<Driver>();
            var listName = "All Active Drivers";
            List<Company> _co = context.Companys.ToList();
            var driverList = new List<DriverCompanyModel>();

            if (id != 0)
            {
                // drivers = context.Drivers.Where(c => c.Company_id == id).ToList();
                drivers = context.Drivers.Where(d => d.Company_id == id && d.TerminationDate == null && !d.isDelete).ToList();
                driverList = drivers.Join(_co,
                                        d => d.Company_id,
                                        co => co.Id,
                                        (driver, company) => new DriverCompanyModel
                                        {
                                            Id = driver.Id,
                                            DriverName = driver.DriverName,
                                            CompanyName = company.Name,
                                            Phone = driver.Phone,
                                            Email = driver.Email,
                                            EnrollmentDate = driver.EnrollmentDate,
                                            TerminationDate = driver.TerminationDate,
                                            isFavorite = driver.isFavorite,
                                        }).ToList();
                listName = context.Companys.Where(c => c.Id == id).FirstOrDefault().Name + " Drivers";
            }
            else
            {
                // drivers = context.Drivers.ToList();
                drivers = context.Drivers.Where(d => d.TerminationDate == null && !d.isDelete).ToList();
                driverList = drivers.Join(_co,
                                        d => d.Company_id,
                                        co => co.Id,
                                        (driver, company) => new DriverCompanyModel
                                        {
                                            Id = driver.Id,
                                            DriverName = driver.DriverName,
                                            CompanyName = company.Name,
                                            Phone = driver.Phone,
                                            Email = driver.Email,
                                            EnrollmentDate = driver.EnrollmentDate,
                                            TerminationDate = driver.TerminationDate,
                                            isFavorite = driver.isFavorite,
                                        }).ToList();

                // 
            }




            var count = drivers.Count();
            ViewBag.ListName = listName;
            _model.Drivers = driverList;

            return View(_model);
        }

        public IActionResult Search(string driverName, string company, string cdl)
        {

            using RDATContext context = new RDATContext();
            DriverSearchModel _model = new DriverSearchModel();
            List<Driver> drivers = new List<Driver>();

            if (driverName != "")
            {
                drivers = context.Drivers.Where(c => c.DriverName.Contains(driverName)).ToList();
            }

            if (!company.IsEmpty() && company != null)
            {
                List<Company> cos = context.Companys.Where(c => c.Name.Contains(company)).ToList();

                foreach (Company co in cos)
                {
                    List<Driver> cosDrivers = context.Drivers.Where(d => d.Company_id == co.Id).ToList();
                    if (cosDrivers != null)
                    {
                        drivers.AddRange(cosDrivers);
                    }
                }

            }

            if (!cdl.IsEmpty() && cdl != null)
            {
                List<Driver> cdlDrivers = drivers.Where(c => c.CDL.Contains(cdl)).ToList();
                if (cdlDrivers != null)
                {
                    drivers.AddRange(cdlDrivers);
                }
            }

            List<Company> _co = context.Companys.ToList();
            var driverList = drivers.Join(_co,
                                        d => d.Company_id,
                                        co => co.Id,
                                        (driver, company) => new DriverCompanyModel
                                        {
                                            Id = driver.Id,
                                            DriverName = driver.DriverName,
                                            CompanyName = company.Name,
                                            Phone = driver.Phone,
                                            Email = driver.Email,
                                            EnrollmentDate = driver.EnrollmentDate,
                                            TerminationDate = driver.TerminationDate,
                                            isFavorite = driver.isFavorite,
                                        }).ToList();

            var count = drivers.Count();

            _model.Drivers = driverList;
            _model.DriverName = driverName;

            return View("Index", _model);
        }

        public ActionResult Create()
        {
            CreateDriverViewModel _model = new CreateDriverViewModel();
            Driver _driver = new Driver();
            _model.Driver = _driver;
            
            using RDATContext context = new RDATContext();
            List<SelectListItem> companies = context.Companys.OrderBy(c => c.Name).Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.Name
                                  }).ToList();
            _model.Companies = companies;

            List<SelectListItem> states = context.States.OrderBy(s => s.StateName).Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.StateName
                                  }).ToList();
            _model.States = states;

            return View(_model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
    [Bind("Company_id,DriverName,SSN,AddressLine1,AddressLine2,City,State,Zipcode,Location,Phone,Fax,Cell,Email,CDL")] Driver driver)
        {
                      
            using RDATContext context = new RDATContext();
            var drivers = context.Drivers;
            try
            {
                if (ModelState.IsValid)
                {
                    driver.UniqueId = Guid.NewGuid();
                    driver.EnrollmentDate = DateTime.Now;
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

            return View();
        }

        public IActionResult Edit(int id)
        {
            using RDATContext context = new RDATContext();

            CreateDriverViewModel _model = new CreateDriverViewModel();

            Driver _driver = context.Drivers.Where(d => d.Id == id).FirstOrDefault();
            ViewBag.CompanyName = _driver.DriverName;

            List<SelectListItem> states = context.States.OrderBy(s => s.StateName).Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.StateName
                                  }).ToList();
            _model.States = states;

            _model.Driver = _driver;

            return View(_model);
        }

        public ActionResult Favorite(int id)
        {
            var thisID = id;

            using RDATContext context = new RDATContext();

            try
            {
                Driver _driver = context.Drivers.Where(c => c.Id == id).FirstOrDefault();

                _driver.isFavorite = !_driver.isFavorite;

                context.Update(_driver);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }


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
            // TODO: Create List of Alcohol Dates - There can be multiples
            List<TestingLog> _logDrug = context.TestingLogs.Where(l => l.Driver_Id == id && l.Test_Type == "Drug" && l.ResultsDate == null).ToList();

            // TODO: Create List of Alcohol Dates - There can be multiples
            List<TestingLog> _logAlcohol = context.TestingLogs.Where(l => l.Driver_Id == id && l.Test_Type == "Alcohol" && l.ResultsDate == null).ToList();

            // Set Values
            _driverResults.Driver_Id = _driver.Id;
            _driverResults.Driver_Name = _driver.DriverName;
            _driverResults.Company_Name = context.Companys.Where(c => c.Id == _driver.Company_id).FirstOrDefault().Name;
            _driverResults.Driver_UniqueID = _driver.UniqueId.ToString();

            // Do we have results
            _driverResults.Alcohol_Show = _logAlcohol.Count > 0 ? true : false;
            _driverResults.Drug_Show = _logDrug.Count > 0 ? true : false;

            // Alcohol Test
            if (_logAlcohol.Count > 0)
            {
                List<AlcoholResult> results = new List<AlcoholResult>();
                foreach(TestingLog tl in _logAlcohol)
                {
                    AlcoholResult alcoholResult = new AlcoholResult();
                    alcoholResult.Alcohol_TestingLogID = tl.Id;
                    alcoholResult.Alcohol_Test_Date = tl.TestDate;
                    alcoholResult.Alcohol_Specimen_Id = tl.Specimen_Id;
                    alcoholResult.Alcohol_Results_Date = tl.ResultsDate;
                    alcoholResult.Alcohol_Reported_Result = tl.Reported_Results;
                    alcoholResult.BatchID = tl.Batch_Id;
                    results.Add(alcoholResult);
                }

                _driverResults.AlcoholResults = results;
                
            }

            // Drug Test
            if (_logDrug.Count > 0)
            {
                List<DrugResult> dresults = new List<DrugResult>();
                foreach (TestingLog tl in _logDrug)
                {
                    DrugResult drugResult = new DrugResult();
                    drugResult.Drug_TestingLogID = tl.Id;
                    drugResult.Drug_Test_Date = tl.TestDate;
                    drugResult.Drug_Specimen_Id = tl.Specimen_Id;
                    drugResult.Drug_Results_Date = tl.ResultsDate;
                    drugResult.Drug_Reported_Result = tl.Reported_Results;
                    drugResult.BatchID = tl.Batch_Id;
                    dresults.Add(drugResult);
                }

                _driverResults.DrugResults = dresults;
            }

            return _driverResults;
        }

        [HttpPost]
        public ActionResult UpdateDriver([FromBody] UpdateDriverResults results)
        {
            using RDATContext context = new RDATContext();

            // Check for Drug Update
            //if (results != null && results.Drug_TestingLogID != 0)
            //{
            //    TestingLog _drugResults = context.TestingLogs.Where(tl => tl.Id == results.Drug_TestingLogID).FirstOrDefault();
            //    _drugResults.ResultsDate = results.Drug_Results_Date;
            //    _drugResults.Reported_Results = results.Drug_Reported_Result;
            //    context.Update(_drugResults);
            //    context.SaveChanges();
            //}

            // Check for Alcohol 
            //if (results != null && results.Alcohol_TestingLogID != 0)
            //{
            //    TestingLog _alcoholResults = context.TestingLogs.Where(tl => tl.Id == results.Alcohol_TestingLogID).FirstOrDefault();
            //    _alcoholResults.ResultsDate = results.Alcohol_Results_Date;
            //    _alcoholResults.Reported_Results = results.Alcohol_Reported_Result;
            //    context.Update(_alcoholResults);
            //    context.SaveChanges();
            //}

            if (results == null)
            {
                //  Send "false"
                return Json(new { success = false, responseText = "The attached file is not supported." });
            }
            else
            {
                //  Send "Success"
                return Json(new { success = true, responseText = "Your message successfuly sent!" });
            }

        }

        public ActionResult GetDriverLogs(int id)
        {
            SingleBatchViewModel _model = new SingleBatchViewModel();
            using (RDATContext context = new RDATContext())
            {
                // List<TestingLog> _logs = context.TestingLogs.Where(tl => tl.Batch_Id == BatchId).ToList();
                List<BatchCompanyModel> _logs = (from tl in context.TestingLogs
                                                 join c in context.Companys on tl.Company_Id equals c.Id
                                                 join b in context.Batches on tl.Batch_Id equals b.Id
                                                 select new BatchCompanyModel()
                                                 {
                                                     Id = tl.Id,
                                                     Driver_Id = tl.Driver_Id,
                                                     Driver_Name = tl.Driver_Name,
                                                     Company_Id = tl.Company_Id,
                                                     Company_Name = c.Name,
                                                     Reported_Results = tl.Reported_Results,
                                                     ResultsDate = tl.ResultsDate,
                                                     Batch_Id = tl.Batch_Id,
                                                     BatchDate = b.RunDate,
                                                     ClosedDate = tl.ClosedDate,
                                                     Test_Type = tl.Test_Type
                                                 }).Where(tl => tl.Driver_Id == id).ToList();

                foreach (BatchCompanyModel l in _logs)
                {
                    l.Reported_Results = l.Reported_Results == "1" ? "Positive" : l.Reported_Results == "2" ? "Negative" : l.Reported_Results == "3" ? "Excused" : " ";
                }

                _model.TestingLogs = _logs;

            }

            return View(_model);
        }


        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            CreateDriverViewModel _model = new CreateDriverViewModel();
            Driver _driver = new Driver();

            try
            {
                using RDATContext context = new RDATContext();

                _driver = context.Drivers.Where(d => d.Id == id).FirstOrDefault();

                _driver.DriverName = collection["Driver.DriverName"];
                _driver.Phone = collection["Driver.Phone"];
                _driver.Fax = collection["Driver.Fax"];
                _driver.AddressLine1 = collection["Driver.AddressLine1"];
                _driver.AddressLine2 = collection["Driver.AddressLine2"];
                _driver.City = collection["Driver.City"];
                _driver.State = collection["Driver.State"];
                _driver.Zipcode = collection["Driver.Zipcode"];
                _driver.Email = collection["Driver.Email"];
                _driver.CDL = collection["Driver.CDL"];
                _driver.Cell = collection["Driver.Cell"];

                if (Int32.TryParse(collection["Driver.Company_id"], out int result))
                {
                    _driver.Company_id = Int32.Parse(collection["Driver.Company_id"]);
                };


                _driver.EnrollmentDate = DateTime.Parse(collection["Driver.EnrollmentDate"]);

                if(DateTime.TryParse(collection["Driver.TerminationDate"], out DateTime resultDate))
                {
                    _driver.TerminationDate = DateTime.Parse(collection["Driver.TerminationDate"]);
                }
                
                context.Update(_driver);
                context.SaveChanges();

                // return data;
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                using RDATContext context = new RDATContext();
                List<SelectListItem> states = context.States.OrderBy(s => s.StateName).Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.StateName
                                  }).ToList();
                _model.States = states;

                _model.Driver = _driver;
                return View(_model);
            }
        }
    }
}