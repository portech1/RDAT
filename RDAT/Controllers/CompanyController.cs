using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RDAT.Data;
using RDAT.Models;
using RDAT.ViewModels;
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

        public IActionResult GetActiveDriversByCompany(int companyId)
        {
            using RDATContext context = new RDATContext();

            var activeDrivers = context.Drivers.Where(d => d.TerminationDate == null && d.Company_id == companyId);
            string json = JsonConvert.SerializeObject(activeDrivers);

            var count = activeDrivers.Count();

            var w = new GridData() { data = json, itemsCount = count };

            string data = JsonConvert.SerializeObject(w);

            // return data;
            return View(json);
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
            CreateCompanyViewModel _model = new CreateCompanyViewModel();
            Company _company = new Company();
            _model.Company = _company;

            using RDATContext context = new RDATContext();

            List<SelectListItem> states = context.States.OrderBy(s => s.StateName).Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.StateName
                                  }).ToList();
            _model.States = states;

            return View(_model);
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
                _company.Name = collection["Company.Name"];
                _company.Phone = collection["Company.Phone"];
                _company.Fax = collection["Company.Fax"];
                _company.AddressLine1 = collection["Company.AddressLine1"];
                _company.AddressLine2 = collection["Company.AddressLine2"];
                _company.City = collection["Company.City"];
                _company.State = collection["Company.Zip"];
                _company.Zip = collection["Company.AddressLine1"];
                _company.Email = collection["Company.Email"];

                _company.Created = DateTime.Now;
                _company.Modified = DateTime.Now;
                _company.isDelete = false;
                _company.Status = "1";

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

            CreateCompanyViewModel _model = new CreateCompanyViewModel();

            Company _company = context.Companys.Where(c => c.Id == id).FirstOrDefault();
            ViewBag.CompanyName = _company.Name;

            List<SelectListItem> states = context.States.OrderBy(s => s.StateName).Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.StateName
                                  }).ToList();
            _model.States = states;

            _model.Company = _company;
            // return data;
            return View(_model);
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

                _company.Name = collection["Company.Name"];
                _company.Phone = collection["Company.Phone"];
                _company.Fax = collection["Company.Fax"];
                _company.AddressLine1 = collection["Company.AddressLine1"];
                _company.AddressLine2 = collection["Company.AddressLine2"];
                _company.City = collection["Company.City"];
                _company.State = collection["Company.State"];
                _company.Zip = collection["Company.Zip"];
                _company.Email = collection["Company.Email"];
                _company.RepresentativeName = collection["Company.RepresentativeName"];
                _company.Status = collection["Company.Status"];
                
                _company.Modified = DateTime.Now;

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