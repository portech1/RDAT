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

        public IActionResult CreateBatch(int limit = 100)
        {
            CreateBatchViewModel _model = new CreateBatchViewModel();
            
            var drivers = new List<Driver>();
            double _percentage = 0;
            ViewBag.limit = limit;
            
            var activeDrivers = 0;

            using (RDATContext context = new RDATContext())
            {
                activeDrivers = context.Drivers.Where(d => d.EnrollmentDate > d.TerminationDate).Count();
                _percentage = (double)limit / activeDrivers;
                _percentage = _percentage * 100;
                ViewBag.percentage = _percentage.ToString("##.##");
                ViewBag.activeDrivers = activeDrivers;
                drivers = context.Drivers.Where(d => d.EnrollmentDate > d.TerminationDate).OrderBy(d => Guid.NewGuid()).Take(limit).ToList();
                var count = drivers.Count();
            };

            _model.drivers = drivers;
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