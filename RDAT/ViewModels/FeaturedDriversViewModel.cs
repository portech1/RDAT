using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDAT.Models;

namespace RDAT.ViewModels
{
    public class FeaturedDriversViewModel
    {
        public String Title { get; set; }

        public String SearchTerm { get; set; }

        public List<Driver> Drivers { get; set; }
    }
}
