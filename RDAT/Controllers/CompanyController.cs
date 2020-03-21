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
            //Task.Run(() => { // or ThreadPool.QueueUserWorkItem(async _ => {

            //    var host = CreateHostBuilder(args).Build(); 
            //    var scope = host.Services.CreateScope();
            //    var services = scope.ServiceProvider;

            //    var context = services.GetRequiredService<RDATContext>();
            //    context.Database.EnsureCreated();

            //    using (context)//RDATContext context1 = new RDATContext())
            //    {
            //        var Companies = context.Companys.OrderBy(c => c.Name);

            //        foreach (Company c in Companies)
            //        {
            //            Console.WriteLine(c.Name);
            //        }

            //        return View(Companies);
            //    }
            //});

            using RDATContext context = new RDATContext();

            var companys = context.Companys;

            var count = companys.Count();

            return View();
        }

        public ActionResult Grid()
        {
            using RDATContext context = new RDATContext();

            var companys = context.Companys;
            
            return View(companys.ToList());
        }

        //public ActionResult Grid(int page = 1)
        //{
        //    const int pageSize = 5;

        //    int totalRecords;
        //    IEnumerable<Product> products = productService.GetProducts(
        //      out totalRecords, pageSize: pageSize, pageIndex: page - 1);

        //    PagedProductsModel model = new PagedProductsModel
        //    {
        //        PageSize = pageSize,
        //        PageNumber = page,
        //        Products = products,
        //        TotalRows = totalRecords
        //    };
        //    return View(model);
        //}


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
                //// TODO: Add insert logic here
                string Name = collection["Name"];
                string Email = collection["Email"];

                using RDATContext context = new RDATContext();

                Company newCo = new Company()
                {
                    Name = Name,
                    Email = Email,
                };

                context.Companys.Add(newCo);

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
            return View();
        }

        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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