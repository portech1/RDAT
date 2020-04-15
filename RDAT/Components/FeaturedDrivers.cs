using Microsoft.AspNetCore.Mvc;
using RDAT.Data;
using RDAT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Components
{
    public class FeaturedDrivers: ViewComponent
    {
        public IViewComponentResult Invoke(string searchTerm)
        {
            FeaturedDriversViewModel _model = new FeaturedDriversViewModel();
            using RDATContext context = new RDATContext();

            var my = searchTerm;

            _model.Drivers = context.Drivers.Where(d => d.isFavorite).OrderByDescending(p => p.Id).ToList();
            return View(_model);
        }
    }
}
