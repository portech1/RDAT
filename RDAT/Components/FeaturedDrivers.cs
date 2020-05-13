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
                int _latestBatchId = context.Batches.OrderByDescending(b => b.Id).FirstOrDefault().Id;
                bool _isInLatestBatch = context.TestingLogs.Where(l => l.Batch_Id == _latestBatchId).Any(tl => tl.Driver_Id == d.Id && tl.Batch_Id == _latestBatchId);

                _results.Add(new DriverSearchResult
                {
                    CompanyName = _companyName,
                    DriverName = d.DriverName,
                    Id = d.Id,
                    isLatestBatch = _isInLatestBatch,
                    isFavorite = d.isFavorite
                });
            }

            _model.Drivers = _results;

            return View(_model);
        }
    }
}
