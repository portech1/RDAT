using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RDAT.Data;
using RDAT.Models;

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

        public IActionResult Driver(int limit = 100)
        {
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

            return View(drivers);
        }

        public IActionResult Company()
        {
            return View();
        }
    }
}