using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RDAT.Data;
using RDAT.Migrations;
using RDAT.Models;
using RDAT.ViewModels;

namespace RDAT.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Closed_Drug_Test()
        {
            return View();
        }

        public IActionResult Closed_Alcohol_Test()
        {
            return View();
        }

        public IActionResult Drivers_Test()
        {
            return View();
        }

        public IActionResult RandomAlcoholPoolSummary()
        {
            List<RandomPool> _summary = new List<RandomPool>();

            RandomPool entry = new RandomPool
            {
                Random_Test_Selection_Date = DateTime.Now,
                Batch_Number = 108,
                Active_Enrolled_Drivers = 545,
                Selected_Drivers = 235,
                Excused_Drivers = 15,
                Positive_Tested_Drivers = 13,
                Negative_Tested_Drivers = 200,
                Selection_Test_Ratio = .10,
                Annual_Ratio = .15
            };

            _summary.Add(entry);

            RandomPool entry2 = new RandomPool
            {
                Random_Test_Selection_Date = DateTime.Now.AddDays(-365),
                Batch_Number = 11,
                Active_Enrolled_Drivers = 333,
                Selected_Drivers = 111,
                Excused_Drivers = 15,
                Positive_Tested_Drivers = 13,
                Negative_Tested_Drivers = 200,
                Selection_Test_Ratio = .09,
                Annual_Ratio = .22
            };

            _summary.Add(entry2);

            RandomPool entry3 = new RandomPool
            {
                Random_Test_Selection_Date = DateTime.Now.AddDays(-800),
                Batch_Number = 10,
                Active_Enrolled_Drivers = 444,
                Selected_Drivers = 222,
                Excused_Drivers = 15,
                Positive_Tested_Drivers = 13,
                Negative_Tested_Drivers = 200,
                Selection_Test_Ratio = .11,
                Annual_Ratio = .13
            };

            _summary.Add(entry3);

            return View(_summary);
        }

        public IActionResult RandomAlcoholPoolDetails(int id = 0)
        {
            List<RandomPoolDetails> _drivers = new List<RandomPoolDetails>();

            // get batch details
            DateTime _selectionDate = DateTime.Now;
            int _batchNumber = 12;
            int _activeEnrolledDrivers = 435;
            double _selectionTestRatio = .23;
            double _annualRatio = .18;

            using (RDATContext context = new RDATContext())
            {
                var drivers = new List<Driver>();
                var listName = "All Drivers";
                List<Company> _co = context.Companys.ToList();
                var driverList = new List<DriverCompanyModel>();

                if (id != 0)
                {
                    drivers = context.Drivers.Where(c => c.Company_id == id).ToList();
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
                                                TerminationDate = driver.TerminationDate
                                            }).ToList();
                    listName = context.Companys.Where(c => c.Id == id).FirstOrDefault().Name + " Drivers";
                }
                else
                {
                    drivers = context.Drivers.ToList();
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
                                                TerminationDate = driver.TerminationDate
                                            }).ToList();

                    // 
                }

                // Create Pool List
                foreach (DriverCompanyModel dm in driverList)
                {
                    _drivers.Add(new RandomPoolDetails
                    {
                        Random_Test_Selection_Date = _selectionDate,
                        Batch_Number = _batchNumber,
                        Active_Enrolled_Drivers = _activeEnrolledDrivers,
                        Company = dm.CompanyName,
                        Selected_Driver = dm.DriverName,
                        Excused_Driver = "Excused",
                        Positive_Tested_Driver = "Positive",
                        Selection_Test_Ratio = _selectionTestRatio,
                        Annual_Ratio = _annualRatio
                    });
                }
            }


            return View(_drivers);
        }

        public IActionResult RandomDrugPoolSummary()
        {
            List<RandomPool> _summary = new List<RandomPool>();

            RandomPool entry = new RandomPool
            {
                Random_Test_Selection_Date = DateTime.Now,
                Batch_Number = 12,
                Active_Enrolled_Drivers = 545,
                Selected_Drivers = 235,
                Excused_Drivers = 15,
                Positive_Tested_Drivers = 13,
                Negative_Tested_Drivers = 200,
                Selection_Test_Ratio = .10,
                Annual_Ratio = .15
            };

            _summary.Add(entry);

            RandomPool entry2 = new RandomPool
            {
                Random_Test_Selection_Date = DateTime.Now.AddDays(-365),
                Batch_Number = 11,
                Active_Enrolled_Drivers = 333,
                Selected_Drivers = 111,
                Excused_Drivers = 15,
                Positive_Tested_Drivers = 13,
                Negative_Tested_Drivers = 200,
                Selection_Test_Ratio = .09,
                Annual_Ratio = .22
            };

            _summary.Add(entry2);

            RandomPool entry3 = new RandomPool
            {
                Random_Test_Selection_Date = DateTime.Now.AddDays(-800),
                Batch_Number = 10,
                Active_Enrolled_Drivers = 444,
                Selected_Drivers = 222,
                Excused_Drivers = 15,
                Positive_Tested_Drivers = 13,
                Negative_Tested_Drivers = 200,
                Selection_Test_Ratio = .11,
                Annual_Ratio = .13
            };

            _summary.Add(entry3);

            return View(_summary);
        }

        public IActionResult RandomDrugPoolDetails(int id = 0)
        {
            List<RandomPoolDetails> _drivers = new List<RandomPoolDetails>();

            // get batch details
            DateTime _selectionDate = DateTime.Now;
            int _batchNumber = 12;
            int _activeEnrolledDrivers = 435;
            double _selectionTestRatio = .23;
            double _annualRatio = .18;

            using (RDATContext context = new RDATContext())
            {
                var drivers = new List<Driver>();
                var listName = "All Drivers";
                List<Company> _co = context.Companys.ToList();
                var driverList = new List<DriverCompanyModel>();

                List<TestingLog> _logs = context.TestingLogs.Where(tl => tl.Batch_Id == id).ToList();

                foreach (TestingLog l in _logs)
                {
                    l.Reported_Results = l.Reported_Results == "1" ? "Positive" : l.Reported_Results == "2" ? "Negative" : l.Reported_Results == "3" ? "Excused" : " ";
                }

                if (id != 0)
                {
                    //drivers = context.Drivers.Where(c => c.Company_id == id).ToList();
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
                                                TerminationDate = driver.TerminationDate
                                            }).ToList();
                    listName = context.Companys.Where(c => c.Id == id).FirstOrDefault().Name + " Drivers";
                }
                else
                {
                    drivers = context.Drivers.ToList();
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
                                                TerminationDate = driver.TerminationDate
                                            }).ToList();

                    // 
                }

                // Create Pool List
                foreach (DriverCompanyModel dm in driverList)
                {
                    _drivers.Add(new RandomPoolDetails
                    {
                        Random_Test_Selection_Date = _selectionDate,
                        Batch_Number = _batchNumber,
                        Active_Enrolled_Drivers = _activeEnrolledDrivers,
                        Company = dm.CompanyName,
                        Selected_Driver = dm.DriverName,
                        Excused_Driver = "Excused",
                        Positive_Tested_Driver = "Positive",
                        Selection_Test_Ratio = _selectionTestRatio,
                        Annual_Ratio = _annualRatio
                    });
                }
            }


            return View(_drivers);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateBatch(CreateBatch batchRequest)
        {
            CreateBatchViewModel _model = new CreateBatchViewModel();
            var driversDrug = new List<Driver>();
            var driversAlcohol = new List<Driver>();
            var _tempTestingLog = new List<TempTestingLog>();

            // Determine whether
            if (batchRequest == null || batchRequest.AlcoholPercentage == 0 && batchRequest.DrugPercentage == 0)
            {
                // Load existing temp log entries
                using (RDATContext context = new RDATContext())
                {
                    // get list of existing test logs
                    _tempTestingLog = context.TempTestingLogs.ToList();
                    // get active drivers
                    //var activeDrivers = context.Drivers.Where(d => d.EnrollmentDate > d.TerminationDate).Count();
                    var activeDrivers = context.Drivers.Where(d => d.TerminationDate == null && !d.isDelete).Count();
                    int numDrug = context.TempTestingLogs.Where(t => t.Test_Type == "Drug").Count();
                    int numAlcohol = context.TempTestingLogs.Where(t => t.Test_Type == "Alcohol").Count();
                    float _percentDrug = ((float)numDrug / activeDrivers) * 100;
                    float _percentAlcohol = ((float)numAlcohol / activeDrivers) * 100;
                    ViewBag.percentDrug = _percentDrug.ToString("##");
                    ViewBag.percentAlcohol = _percentAlcohol.ToString("##");
                    ViewBag.totalDrivers = numDrug + numAlcohol;
                    ViewBag.activeDrivers = activeDrivers;
                }
            }
            else
            {
                // generate new entries
                float _percentDrug = (float)batchRequest.DrugPercentage / 100;
                float _percentAlcohol = (float)batchRequest.AlcoholPercentage / 100;
                double _percentage = 0;
                ViewBag.percentDrug = batchRequest.DrugPercentage;
                ViewBag.percentAlcohol = batchRequest.AlcoholPercentage;

                var activeDrivers = 0;

                using (RDATContext context = new RDATContext())
                {
                    List<TempTestingLog> existingEntries = context.TempTestingLogs.ToList();
                    // First get rid of old entries
                    context.TempTestingLogs.RemoveRange(existingEntries);

                    activeDrivers = context.Drivers.Where(d => d.TerminationDate == null && !d.isDelete).Count();

                    int numDrug = (int)(_percentDrug * activeDrivers);
                    int numAlcohol = (int)(_percentAlcohol * activeDrivers);
                    // _percentage = (double)numDrug / activeDrivers;
                    // _percentage = _percentage * 100;
                    ViewBag.percentage = _percentage.ToString("##.##");
                    ViewBag.activeDrivers = activeDrivers;
                    driversDrug = context.Drivers.Where(d => d.TerminationDate == null && !d.isDelete).OrderBy(d => Guid.NewGuid()).Take(numDrug).ToList();
                    driversAlcohol = context.Drivers.Where(d => d.TerminationDate == null && !d.isDelete).OrderBy(d => Guid.NewGuid()).Take(numAlcohol).ToList();

                    foreach (Driver d in driversDrug)
                    {
                        TempTestingLog _tempLog = new TempTestingLog();
                        _tempLog.Test_Type = "Drug";
                        _tempLog.Driver_Id = d.Id;
                        _tempLog.Driver_Name = d.DriverName;
                        _tempLog.Company_Id = d.Company_id;
                        _tempLog.CreatedDate = DateTime.Now;
                        _tempLog.ModifiedDate = DateTime.Now;
                        // context.TempTestingLogs.Add(_tempLog);
                        _tempTestingLog.Add(_tempLog);
                    }

                    foreach (Driver d in driversAlcohol)
                    {
                        TempTestingLog _tempLog = new TempTestingLog();
                        _tempLog.Test_Type = "Alcohol";
                        _tempLog.Driver_Id = d.Id;
                        _tempLog.Driver_Name = d.DriverName;
                        _tempLog.Company_Id = d.Company_id;
                        _tempLog.CreatedDate = DateTime.Now;
                        _tempLog.ModifiedDate = DateTime.Now;
                        // context.TempTestingLogs.Add(_tempLog);
                        _tempTestingLog.Add(_tempLog);
                    }

                    // Add new records to database


                    var count = _tempTestingLog.Count();
                    ViewBag.totalDrivers = count;

                    foreach (TempTestingLog log in _tempTestingLog)
                    {
                        context.TempTestingLogs.Add(log);
                    }
                    context.SaveChanges();

                };


            }


            _model.tempTestingLogs = _tempTestingLog;
            _model.batchRequest = new CreateBatch();

            // Old model on page was --> IEnumerable<RDAT.Models.Driver>

            return View(_model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //// public async Task<IActionResult> CreateBatch([Bind("id,ActivePool,DrugPercentage,DrugTestDate,AlcoholPercentage,AlcoholTestDate")] CreateBatch createBatch)
        //public async Task<IActionResult> UpdateBatch(CreateBatchViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //_context.Add(createBatch);
        //        //await _context.SaveChangesAsync();
        //        //return RedirectToAction(nameof(Index));
        //    }
        //    var me = model.batchRequest.ActivePool;
        //    return View(model);
        //}


        [Authorize(Roles = "Admin")]
        public IActionResult UpdateBatch(CreateBatchViewModel model)
        {
            // Create Batch

            Batch _batch = new Batch();
            var _tempTestingLog = new List<TempTestingLog>();

            using (RDATContext context = new RDATContext())
            {
                // get list of existing test logs
                _tempTestingLog = context.TempTestingLogs.ToList();
                // get active drivers
                int activeDrivers = context.Drivers.Where(d => d.TerminationDate == null && !d.isDelete).Count();
                int numDrug = context.TempTestingLogs.Where(t => t.Test_Type == "Drug").Count();
                int numAlcohol = context.TempTestingLogs.Where(t => t.Test_Type == "Alcohol").Count();
                float _percentDrug = ((float)numDrug / activeDrivers) * 100;
                float _percentAlcohol = ((float)numAlcohol / activeDrivers) * 100;
                ViewBag.percentDrug = _percentDrug.ToString("##");
                ViewBag.percentAlcohol = _percentAlcohol.ToString("##");
                ViewBag.totalDrivers = numDrug + numAlcohol;
                ViewBag.activeDrivers = activeDrivers;

                // Create the Batch
                _batch.Created = DateTime.Now;
                _batch.RunDate = DateTime.Now;
                _batch.Alcohol_Percentage = (int)Math.Round(_percentAlcohol);
                _batch.Drug_Percentage = (int)Math.Round(_percentDrug);
                _batch.Alcohol_Tests = numAlcohol;
                _batch.Drug_Tests = numDrug;
                _batch.Eligible_Drivers = activeDrivers;

                context.Batches.Add(_batch);
                context.SaveChanges();

                // Get New Batch ID
                int batchID = _batch.Id;
                // Build Batch
                BuildBatch(batchID);
                ClearTempBatch();
            }

            return RedirectToAction("ViewBatch", "Reports");
        }

        [Authorize(Roles = "Admin")]
        private bool BuildBatch(int id)
        {
            using (RDATContext context = new RDATContext())
            {
                List<TempTestingLog> _logs = context.TempTestingLogs.ToList();

                foreach (TempTestingLog log in _logs)
                {
                    TestingLog _testingLog = new TestingLog();
                    _testingLog.Test_Type = log.Test_Type;
                    _testingLog.Driver_Id = log.Driver_Id;
                    _testingLog.Driver_Name = log.Driver_Name;
                    _testingLog.Created = log.CreatedDate;
                    _testingLog.Selectiondatealcohol = log.CreatedDate;
                    _testingLog.Selectiondatedrug = log.CreatedDate;
                    _testingLog.Modified = log.ModifiedDate;
                    _testingLog.TestDate = log.CreatedDate;
                    _testingLog.Batch_Id = id;
                    _testingLog.Company_Id = log.Company_Id;
                    _testingLog.Reported_Results = "0";

                    context.TestingLogs.Add(_testingLog);

                }

                context.SaveChanges();
                return true;
            }

            return false;
        }

        [Authorize(Roles = "Admin")]
        private bool ClearTempBatch()
        {

            try
            {
                using (RDATContext context = new RDATContext())
                {
                    List<TempTestingLog> existingEntries = context.TempTestingLogs.ToList();
                    context.TempTestingLogs.RemoveRange(existingEntries);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public IActionResult Company()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult saveTestingLog(int id, string propertyName, string value)
        {
            var status = false;
            var message = "";

            // Update data to database
            using (RDATContext context = new RDATContext())
            {
                var _log = context.TestingLogs.Find(id);

                object updateValue = value;
                bool isValid = true;

                if (propertyName == "Reported_Results")
                {
                    int newResultID = 0;
                    if (int.TryParse(value, out newResultID))
                    {
                        updateValue = newResultID;
                        //Update value field
                        value = context.Results.Where(a => a.Id == newResultID).First().DisplayName;
                    }
                    else
                    {
                        isValid = false;
                    }

                }
                else if (propertyName == "ResultsDate" || propertyName == "TestDate" || propertyName == "ClosedDate")
                {
                    DateTime mod;
                    if (DateTime.TryParseExact(value, "dd-MM-yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out mod))
                    {
                        updateValue = mod;
                    }
                    else
                    {
                        isValid = false;
                    }
                }

                if (_log != null && isValid)
                {
                    if (propertyName == "ResultsDate" || propertyName == "TestDate" || propertyName == "ClosedDate")
                    {
                        context.Entry(_log).Property(propertyName).CurrentValue = (DateTime)updateValue;
                    }
                    else if (propertyName == "Specimen_Id")
                    {
                        context.Entry(_log).Property(propertyName).CurrentValue = Convert.ToInt32(updateValue);
                    }
                    else
                    {
                        context.Entry(_log).Property(propertyName).CurrentValue = updateValue.ToString();
                    }
                    context.SaveChanges();
                    status = true;
                }
                else
                {
                    message = "Error!";
                }
            }

            var response = new { value = value, status = status, message = message };
            JObject o = JObject.FromObject(response);
            return Content(o.ToString());
        }


        // Show All Batches
        public IActionResult ViewBatch()
        {
            ViewBatchViewModel _model = new ViewBatchViewModel();
            using (RDATContext context = new RDATContext())
            {
                _model.Batches = context.Batches.OrderByDescending(o => o.Id).ToList();
            }

            return View(_model);
        }

        public ActionResult GetResults(string currentResult)
        {
            //Allowed response content format
            //{'E':'Letter E','F':'Letter F','G':'Letter G', 'selected':'F'}
            //"{'3':'Excused','2':'Negative','1':'Positive','4':'Undefined','selected': '4'}"
            int selectedResult = 0;
            if (currentResult == null)
            {
                currentResult = "Undefined";
            }

            StringBuilder sb = new StringBuilder();
            using (RDATContext context = new RDATContext())
            {
                var results = context.Results.OrderBy(a => a.DisplayName).ToList();
                foreach (var item in results)
                {
                    sb.Append(string.Format("'{0}':'{1}',", item.Id, item.DisplayName));
                }

                selectedResult = context.Results.Where(a => a.DisplayName == currentResult).First().Id;

            }

            sb.Append(string.Format("'selected':'{0}'", selectedResult));
            //var _json = JsonConvert.SerializeObject("{" + sb.ToString() + "}");
            //return Json(_json);
            return Content("{" + sb.ToString() + "}");
        }


        public IActionResult SingleBatch(int BatchId)
        {
            SingleBatchViewModel _model = new SingleBatchViewModel();
            using (RDATContext context = new RDATContext())
            {

                ViewBag.BatchId = BatchId;
                ViewBag.Created = context.Batches.Where(b => b.Id == BatchId).FirstOrDefault().Created;
                ViewBag.RunDate = context.Batches.Where(b => b.Id == BatchId).FirstOrDefault().RunDate;

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
                                                 }).Where(tl => tl.Batch_Id == BatchId).ToList();

                foreach (BatchCompanyModel l in _logs)
                {
                    l.Reported_Results = l.Reported_Results == "1" ? "Positive" : l.Reported_Results == "2" ? "Negative" : l.Reported_Results == "3" ? "Excused" : " ";
                }

                _model.TestingLogs = _logs;
            }

            return View(_model);
        }

        public IActionResult Outstanding(string type)
        {
            SingleBatchViewModel _model = new SingleBatchViewModel();
            ViewBag.type = type;
            using (RDATContext context = new RDATContext())
            {

                List<BatchCompanyModel> _logs = new List<BatchCompanyModel>();

                //List<Company> _co = context.Companys.ToList();

                //List<TestingLog> testingLogs = context.TestingLogs.Where(tl => tl.Test_Type == type && Convert.ToInt32(tl.Reported_Results) < 1).ToList();

                _logs = (from tl in context.TestingLogs
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
                             ClosedDate = tl.ClosedDate,
                             Batch_Id = tl.Batch_Id,
                             BatchDate = b.RunDate,
                             Test_Type = tl.Test_Type
                         }).Where(tl => tl.Test_Type == type && Convert.ToInt32(tl.Reported_Results) < 1).ToList();


                //_logs = testingLogs.Join(_co,
                //                            d => d.Company_Id,
                //                            co => co.Id,
                //                            (log, company) => new BatchCompanyModel
                //                            {
                //                                // Id = ,
                //                                Driver_Id = log.Driver_Id,
                //                                Driver_Name = log.Driver_Name,
                //                                Company_Id = log.Company_Id,
                //                                Company_Name = company.Name,
                //                                Reported_Results = log.Reported_Results,
                //                                ResultsDate = log.ResultsDate,
                //                                Batch_Id = log.Batch_Id,
                //                                // BatchDate = ,
                //                                Test_Type = log.Test_Type
                //                            }).ToList();



                foreach (BatchCompanyModel l in _logs)
                {
                    l.Reported_Results = l.Reported_Results == "1" ? "Positive" : l.Reported_Results == "2" ? "Negative" : l.Reported_Results == "3" ? "Excused" : " ";
                }

                _model.TestingLogs = _logs.OrderBy(o => o.Driver_Name).ToList();
            }

            return View(_model);
        }

        public List<SelectListItem> GetCompanies()
        {
            using RDATContext context = new RDATContext();
            List<SelectListItem> companies = context.Companys.OrderBy(c => c.Name).Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.Name
                                  }).ToList();

            return companies;
        }

        public IActionResult MIS(string type)
        {
            MISReportViewModel _model = new MISReportViewModel();
            List<SummaryTestType> _SummaryTestTypes = new List<SummaryTestType>();
            
            _model.Companies = GetCompanies();

            _model.SummaryTestTypes = _SummaryTestTypes;
            _model.Details = new List<MISDetails>();
            _model.Totals = new List<SummaryTestType>();

            return View(_model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MIS(MISReportViewModel model)
        {
            // Call Stored Procedure
            // exec [dbo].[MISREPORT] 1,'Drug','20000101','20211231'
            List<SummaryTestType> _list = new List<SummaryTestType>();
            List<SummaryTestType> _totalsList = new List<SummaryTestType>();
            List<MISDetails> _details = new List<MISDetails>();

            DateTime startDate = model.ReportRequest.StartDate;
            DateTime endDate = model.ReportRequest.EndDate;

            // Use Company in Details Search
            bool useCompany = model.ReportRequest.CompanyID == 0 ? false : true;

            // Get Totals
            int total_Active_Enrolled_Drivers = 0;
            int total_Selected_Drivers = 0;
            int total_Excused_Drivers = 0;
            int total_Positive_Tested_Drivers = 0;
            int total_Negative_Tested_Drivers = 0;
            int row_count = 0;

            // Get Companies
            model.Companies = GetCompanies();

            // string _query = "Select b.RunDate as 'Random Test Selection',b.Id as 'Batch Number',b.Eligible_Drivers as 'Active Enrolled Drivers',(b.Drug_Tests + b.Alcohol_Tests) as 'Selected Drivers',(Select count([Batch_Id]) from[dbo].[TestingLogs] WHERE Batch_Id = b.Id AND Reported_Results = 3) as 'Excused Drivers',(Select count([Batch_Id]) from[dbo].[TestingLogs] WHERE Batch_Id = b.Id AND Reported_Results = 1) as 'Positive Drivers',(Select count([Batch_Id]) from[dbo].[TestingLogs] WHERE Batch_Id = b.Id AND Reported_Results = 2) as 'Negative Drivers',(Select count([Batch_Id]) from[dbo].[TestingLogs] WHERE Batch_Id = b.Id AND Reported_Results in (1, 2, 3)) as 'Total',CAST((CAST((Select count([Batch_Id]) from[dbo].[TestingLogs] WHERE Batch_Id = b.Id AND Reported_Results in (1, 2, 3)) AS float) / CAST((b.Drug_Tests + b.Alcohol_Tests) AS float)) AS float) as 'Selection Test Ratio', * from Batches b";

            using (SqlConnection conn = new SqlConnection("Server=tcp:nfssql01.database.windows.net,1433;Initial Catalog=NFS_SQL_01;Persist Security Info=False;User ID=nfsadmin;Password=IFTAtaxes2017;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[MISREPORT]", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                // cmd.Parameters.Add(new SqlParameter("@CompanyID", 1));
                cmd.Parameters.Add(new SqlParameter("@TestType", model.ReportRequest.TestType));
                cmd.Parameters.Add(new SqlParameter("@StartDate", startDate.ToString("yyyyMMdd")));
                cmd.Parameters.Add(new SqlParameter("@EndDate", endDate.ToString("yyyyMMdd")));

                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {
                        var test = rdr.GetValue(0);
                        var test2 = rdr.GetValue("Selected Drivers");


                        if (rdr != null)
                        {
                            int temp_Active_Enrolled_Drivers = Convert.ToInt32(rdr.GetValue("Active Enrolled Drivers"));
                            int temp_Selected_Drivers = Convert.ToInt32(rdr.GetValue("Selected Drivers"));
                            int temp_Excused_Drivers = Convert.ToInt32(rdr.GetValue("Excused Drivers"));
                            int temp_Positive_Tested_Drivers = Convert.ToInt32(rdr.GetValue("Positive Drivers"));
                            int temp_Negative_Tested_Drivers = Convert.ToInt32(rdr.GetValue("Negative Drivers"));
                            
                            double _positive = Convert.ToDouble(temp_Positive_Tested_Drivers);
                            double _negative = Convert.ToDouble(temp_Negative_Tested_Drivers);
                            double _enrolled = Convert.ToDouble(temp_Active_Enrolled_Drivers);
                            double _allResults = _positive + _negative;


                            double temp_Annual_Ratio = Convert.ToDouble(_allResults / _enrolled)*100;

                            total_Active_Enrolled_Drivers += temp_Active_Enrolled_Drivers;
                            total_Selected_Drivers += temp_Selected_Drivers;
                            total_Excused_Drivers += temp_Excused_Drivers;
                            total_Positive_Tested_Drivers += temp_Positive_Tested_Drivers;
                            total_Negative_Tested_Drivers += temp_Negative_Tested_Drivers;
                            
                            row_count += 1;

                            SummaryTestType row = new SummaryTestType
                            {
                                Description = GetValue<string>(rdr.GetValue("Random Test Selection")),
                                Random_Test_Selection = GetValue<DateTime>(rdr.GetValue("Random Test Selection")),
                                Batch_Number = GetValue<int>(rdr.GetValue("Batch Number")),
                                Active_Enrolled_Drivers = temp_Active_Enrolled_Drivers,
                                Selected_Drivers = temp_Selected_Drivers,
                                Excused_Drivers = temp_Excused_Drivers,
                                Positive_Tested_Drivers = temp_Positive_Tested_Drivers,
                                Negative_Tested_Drivers = temp_Negative_Tested_Drivers,
                                Selection_Test_Ratio = Convert.ToDouble(rdr.GetValue("Selection Test Ratio").ToString()),
                                // Annual_Ratio = Convert.ToDouble(rdr.GetValue("Annual Ratio").ToString())
                                Annual_Ratio = temp_Annual_Ratio
                            };

                            _list.Add(row);
                        }
                    }

                    // Add Totals Row
                    double _positiveTotal = Convert.ToDouble(total_Positive_Tested_Drivers);
                    double _negativeTotal = Convert.ToDouble(total_Negative_Tested_Drivers);
                    double _enrolledTotal = Convert.ToDouble(total_Active_Enrolled_Drivers / row_count);
                    double _allResultsTotal = _positiveTotal + _negativeTotal;


                    double temp_Annual_RatioTotal = Convert.ToDouble(_allResultsTotal / _enrolledTotal) * 100;


                    SummaryTestType _totals = new SummaryTestType
                    {
                        Description = "Totals",
                        Active_Enrolled_Drivers = Convert.ToInt32(total_Active_Enrolled_Drivers/ row_count),
                        Selected_Drivers = total_Selected_Drivers,
                        Excused_Drivers = total_Excused_Drivers,
                        Positive_Tested_Drivers = total_Positive_Tested_Drivers,
                        Negative_Tested_Drivers = total_Negative_Tested_Drivers,
                        Annual_Ratio = temp_Annual_RatioTotal
                    };

                    
                    _totalsList.Add(_totals);

                    _list.Add(_totals);

                    model.Totals = _totalsList;

                    model.SummaryTestTypes = _list;
                }


                // Determine if you need details
                if(model.ReportRequest.IncludeDriverDetails == true)
                {
                    if(useCompany == false)
                    {
                        // GET DETAILS
                        cmd = new SqlCommand("[dbo].[MISREPORT_DETAILS]", conn);
                    }
                    else
                    {
                        // GET DETAILS WITH COMPANY
                        cmd = new SqlCommand("[dbo].[MISREPORT_DETAILS_BY_COMPANY]", conn);
                    }
                                      

                    // 2. set the command object so it knows to execute a stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@TestType", model.ReportRequest.TestType));
                    cmd.Parameters.Add(new SqlParameter("@StartDate", startDate.ToString("yyyyMMdd")));
                    cmd.Parameters.Add(new SqlParameter("@EndDate", endDate.ToString("yyyyMMdd")));
                    
                    if (useCompany == true)
                    {
                        // GET DETAILS
                        cmd.Parameters.Add(new SqlParameter("@Company", model.ReportRequest.CompanyID));
                    }

                    // execute the command
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        // iterate through results, printing each to console
                        while (rdr.Read())
                        {

                            if (rdr != null)
                            {
                                MISDetails row = new MISDetails
                                {
                                    Random_Test_Selection_Date = GetValue<DateTime>(rdr.GetValue("Random_Test_Selection_Date")),
                                    Batch_Number = GetValue<int>(rdr.GetValue("Batch_Number")),
                                    Driver_Name = GetValue<string>(rdr.GetValue("Driver_Name")),
                                    Company_Name = GetValue<string>(rdr.GetValue("Name")),
                                    Test_Type = GetValue<string>(rdr.GetValue("Test_Type")),
                                    Result = GetValue<string>(rdr.GetValue("Result")),
                                    ResultsDate = GetValue<DateTime>(rdr.GetValue("ResultsDate"))
                                };

                                _details.Add(row);
                            }
                        }

                        model.Details = _details;

                        if(!model.ReportRequest.IncludeAllDriverDetails)
                        {
                            model.Details = _details.Where(d => d.Result == "Positive").ToList();
                        }
                    }
                    // END GET DETAILS
                }
                else
                {
                    // Add blank set of details
                    model.Details = _details;
                }

            }
                        

            if (ModelState.IsValid)
            {
                //_context.Add(createBatch);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // Base methods
        protected T GetValue<T>(object obj)
        {
            if (typeof(DBNull) != obj.GetType())
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            return default(T);
        }

        protected T GetValue<T>(object obj, object defaultValue)
        {
            if (typeof(DBNull) != obj.GetType())
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            return (T)defaultValue;
        }

        public IActionResult Renewal()
        {
            RenewalReportViewModel _model = new RenewalReportViewModel();            
            
            using RDATContext context = new RDATContext();
            List<Company> _co = context.Companys.ToList();
            List<Driver> drivers = new List<Driver>();

            int thisMonth = DateTime.Now.Month;
            _model.RenewalMonth = thisMonth;

            drivers = context.Drivers.Where(c => c.TerminationDate == null && c.EnrollmentDate.Value.Month == thisMonth).ToList();
            
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
                                        }).OrderBy(d => d.DriverName).ToList();

            _model.Drivers = driverList;
            
            return View(_model);
        }

        [HttpPost]
        public FileResult Export(string GridHtml)
        {
            return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.ms-excel", "MIS_REPORT.xls");
        }

        [HttpPost]
        public FileResult ExportDetails(string DetailsGridHtml)
        {
            return File(Encoding.ASCII.GetBytes(DetailsGridHtml), "application/vnd.ms-excel", "MIS_REPORT_DETAILS.xls");
        }

        [HttpPost]
        public FileResult ExportPDF(string GridHtml)
        {
            return File(Encoding.ASCII.GetBytes(GridHtml), "application/pdf", "document.pdf");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Renewal(RenewalReportViewModel model)
        {
            RenewalReportViewModel _model = new RenewalReportViewModel();


            using RDATContext context = new RDATContext();
            List<Company> _co = context.Companys.ToList();
            List<Driver> drivers = new List<Driver>();

            _model.RenewalMonth = model.RenewalMonth;

            drivers = context.Drivers.Where(c => c.TerminationDate == null && c.EnrollmentDate.Value.Month == model.RenewalMonth).ToList();

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
                                        }).OrderBy(d => d.DriverName).ToList();

            _model.Drivers = driverList;

            return View(_model);

        }

        }
}


