using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RDAT.Data;
using RDAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RDAT.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index(string[] args)
        {
            using RDATContext context = new RDATContext();

            var companys = context.Companys;

            return View(companys.ToList());
        }

        public ActionResult DashboardCompanies()
        {
            using RDATContext context = new RDATContext();

            var companys = context.Companys.OrderByDescending(p => p.Id).Take(5);

            return View(companys.ToList());
        }

        public JsonResult GetCompanies()
        {
            using RDATContext context = new RDATContext();

            var companys = context.Companys;
            string json = JsonConvert.SerializeObject(companys);

            var count = companys.Count();

            var w = new GridData() { data = json, itemsCount = count };

            string data = JsonConvert.SerializeObject(w);

            // return data;
            return Json(json);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        // GET: Company/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Favorite(int id)
        {
            var thisID = id;

            using RDATContext context = new RDATContext();

            Company _company = context.Companys.Where(c => c.Id == id).FirstOrDefault();

            _company.isFavorite = !_company.isFavorite;

            context.Update(_company);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Company/List
        public ActionResult List(int id)
        {
            using RDATContext context = new RDATContext();

            var companys = context.Companys;

            var count = companys.Count();

            var tempList = new List<Company>();
            var co1 = new Company();
            co1.Name = "Chris";
            tempList.Add(co1);

            return View(tempList);
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Company _company = new Company();

                // Add Company
                _company.Name = collection["Name"];
                _company.Phone = collection["Phone"];
                _company.Fax = collection["Fax"];
                _company.AddressLine1 = collection["AddressLine1"];
                _company.AddressLine2 = collection["AddressLine2"];
                _company.City = collection["City"];
                _company.State = collection["Zip"];
                _company.Zip = collection["AddressLine1"];
                _company.Email = collection["Email"];

                using RDATContext context = new RDATContext();

                context.Companys.Add(_company);
                context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int id)
        {
            using RDATContext context = new RDATContext();

            Company _company = context.Companys.Where(c => c.Id == id).FirstOrDefault();
            ViewBag.CompanyName = _company.Name;

            // return data;
            return View(_company);
        }

        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                using RDATContext context = new RDATContext();

                Company _company = context.Companys.Where(c => c.Id == id).FirstOrDefault();

                _company.Name = collection["Name"];
                _company.Phone = collection["Phone"];
                _company.Fax = collection["Fax"];
                _company.AddressLine1 = collection["AddressLine1"];
                _company.AddressLine2 = collection["AddressLine2"];
                _company.City = collection["City"];
                _company.State = collection["State"];
                _company.Zip = collection["Zip"];
                _company.Email = collection["Email"];
                                
                context.Update(_company);
                context.SaveChanges();

                // return data;
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Company/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}