using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
                    // First get rid of old entries
                    List<TempTestingLog> existingEntries = context.TempTestingLogs.ToList();
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
                        context.TempTestingLogs.Add(_tempLog);
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
                        context.TempTestingLogs.Add(_tempLog);
                        _tempTestingLog.Add(_tempLog);
                    }

                    // Add new records to database


                    var count = _tempTestingLog.Count();
                    ViewBag.totalDrivers = count;

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
        public IActionResult ViewBatch()
        {
            ViewBatchViewModel _model = new ViewBatchViewModel();
            using (RDATContext context = new RDATContext())
            {
                _model.Batches = context.Batches.ToList();
            }

            return View(_model);
        }
    }
}