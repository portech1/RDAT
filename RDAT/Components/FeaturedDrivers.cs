using Microsoft.AspNetCore.Mvc;
using RDAT.Data;
using RDAT.Models;
using RDAT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Components
{
    public class FeaturedDrivers : ViewComponent
    {
        public IViewComponentResult Invoke(string searchTerm)
        {
            FeaturedDriversViewModel _model = new FeaturedDriversViewModel();
            List<Driver> _drivers = new List<Driver>();
            List<DriverSearchResult> _results = new List<DriverSearchResult>();

            using RDATContext context = new RDATContext();

            var my = searchTerm;
            if (searchTerm != null)
            {
                _drivers = context.Drivers.Where(d => d.DriverName.Contains(searchTerm)).OrderByDescending(p => p.Id).ToList();
            }
            else
            {
                _drivers = context.Drivers.Where(d => d.isFavorite).OrderByDescending(p => p.Id).ToList();
            }

            foreach (Driver d in _drivers)
            {
                string _companyName = context.Companys.Where(c => c.Id == d.Company_id).FirstOrDefault().Name;
                
                // Get latest batch - 0 is no batches exist
                int _latestBatchId = context.Batches.OrderByDescending(b => b.Id).FirstOrDefault() != null ? context.Batches.OrderByDescending(b => b.Id).FirstOrDefault().Id  : 0;
                
                // Determine if the Driver was in the last batch - check for 0 first
                bool _isInLatestBatch = _latestBatchId != 0 ? context.TestingLogs.Where(l => l.Batch_Id == _latestBatchId).Any(tl => tl.Driver_Id == d.Id && tl.Batch_Id == _latestBatchId) : false;

                // Look in any batch
                bool _isInAnyBatch = context.TestingLogs.Where(tl => tl.Driver_Id == d.Id && tl.ResultsDate == null).Count() > 0 ? true : false;


                _results.Add(new DriverSearchResult
                {
                    CompanyName = _companyName,
                    DriverName = d.DriverName,
                    TerminationDate = d.TerminationDate,
                    EnrollmentDate = d.EnrollmentDate,
                    Id = d.Id,
                    isLatestBatch =  _isInAnyBatch,// _isInLatestBatch,
                    isFavorite = d.isFavorite
                }); 
                
               
            }

            _model.Drivers = _results;

            return View(_model);
        }
    }
}
