using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDAT.Models;

namespace RDAT.ViewModels
{
    public class FeaturedCompaniesViewModel
    {
        public String Title { get; set; }

        public String SearchTerm { get; set; }

        public List<Company> Companies { get; set; }
    }
}
