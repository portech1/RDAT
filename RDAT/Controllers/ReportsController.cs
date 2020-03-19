using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Driver()
        {
            return View();
        }

        public IActionResult Company()
        {
            return View();
        }
    }
}