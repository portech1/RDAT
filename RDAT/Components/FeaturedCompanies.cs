using Microsoft.AspNetCore.Mvc;
using RDAT.Data;
using RDAT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Components
{
    public class FeaturedCompanies: ViewComponent
    {
        public IViewComponentResult Invoke(string searchTerm)
        {
            FeaturedCompaniesViewModel _model = new FeaturedCompaniesViewModel();
            using RDATContext context = new RDATContext();

            var my = searchTerm;
            if(searchTerm != null)
            {
                _model.Companies = context.Companys.Where(c => c.Name.Contains(searchTerm)).OrderByDescending(p => p.Id).ToList();
            }
            else
            {
                _model.Companies = context.Companys.Where(c => c.isFavorite).OrderByDescending(p => p.Id).ToList();
            }
           
            return View(_model);
        }
    }
}
