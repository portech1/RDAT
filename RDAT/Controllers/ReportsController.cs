using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RDAT.Data;
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
                    var activeDrivers = context.Drivers.Where(d => d.EnrollmentDate > d.TerminationDate).Count();
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

                    activeDrivers = context.Drivers.Where(d => d.EnrollmentDate > d.TerminationDate).Count();
                    int numDrug = (int)(_percentDrug * activeDrivers);
                    int numAlcohol = (int)(_percentAlcohol * activeDrivers);
                    // _percentage = (double)numDrug / activeDrivers;
                    // _percentage = _percentage * 100;
                    ViewBag.percentage = _percentage.ToString("##.##");
                    ViewBag.activeDrivers = activeDrivers;
                    driversDrug = context.Drivers.Where(d => d.EnrollmentDate > d.TerminationDate).OrderBy(d => Guid.NewGuid()).Take(numDrug).ToList();
                    driversAlcohol = context.Drivers.Where(d => d.EnrollmentDate > d.TerminationDate).OrderBy(d => Guid.NewGuid()).Take(numAlcohol).ToList();

                    foreach (Driver d in driversDrug)
                    {
                        TempTestingLog _tempLog = new TempTestingLog();
                        _tempLog.Test_Type = "Drug";
                        _tempLog.Driver_Id = d.Id;
                        _tempLog.Driver_Name = d.DriverName;
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
                        _tempLog.CreatedDate = DateTime.Now;
                        _tempLog.ModifiedDate = DateTime.Now;
                        // context.TempTestingLogs.Add(_tempLog);
                        _tempTestingLog.Add(_tempLog);
                    }

                    // Add new records to database


                    var count = _tempTestingLog.Count();
                    ViewBag.totalDrivers = count;

                    foreach(TempTestingLog log in _tempTestingLog)
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
                int activeDrivers = context.Drivers.Where(d => d.EnrollmentDate > d.TerminationDate).Count();
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
                _batch.Alcohol_Percentage = (int)_percentAlcohol;
                _batch.Drug_Percentage = (int)_percentDrug;
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
                    _testingLog.Modified = log.ModifiedDate;
                    _testingLog.Batch_Id = id;

                    context.TestingLogs.Add(_testingLog);
                                                         
                }

                context.SaveChanges();
                return true;
            }

            return false;
        }

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
                    else {
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
            if(currentResult == null)
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

                List<TestingLog> _logs = context.TestingLogs.Where(tl => tl.Batch_Id == BatchId).ToList();

                foreach(TestingLog l in _logs)
                {
                    l.Reported_Results = l.Reported_Results == "1" ? "Positive" : l.Reported_Results == "2" ? "Negative" : l.Reported_Results == "3" ? "Excused" : " ";
                }

                _model.TestingLogs = _logs;
            }

            return View(_model);
        }
    }
}