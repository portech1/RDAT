using RDAT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Models
{
    public class DashboardViewModel {
        public int BadgeTotalActiveDrivers { get; set; }

        public int BadgeTotalActiveCompanies { get; set; }

        public int BadgeOutstandingDrugTest { get; set; }

        public int BadgeOutstandingAlcoholTest { get; set; }

        public List<Driver> LatestDrivers { get; set; }

        public List<Company> LatestCompanies { get; set; }

        public List<Company> FavoriteCompanies { get; set; }
        
        public List<Driver> FavoriteDrivers { get; set; }

        public String SearchDrivers { get; set; }


        public String SearchCompanies { get; set; }



    }
}
