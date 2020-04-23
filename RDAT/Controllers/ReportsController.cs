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
            if (ModelState.IsValid)
            {
                //_context.Add(createBatch);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            var me = model.batchRequest.ActivePool;
            return View(model);
        }

        public IActionResult Company()
        {
            return View();
        }
    }
}