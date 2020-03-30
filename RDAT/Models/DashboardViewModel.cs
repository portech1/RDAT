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
        public int BadgeTotalNumberDrivers { get; set; }

        public int BadgeTotalDriversForTest { get; set; }

        public int BadgeClosedDrugTest { get; set; }

        public int BadgeClosedAlcoholTest { get; set; }

        public List<Driver> LatestDrivers { get; set; }

        public List<Company> LatestCompanies { get; set; }

        public List<Company> FavoriteCompanies { get; set; }
        
        public List<Driver> FavoriteDrivers { get; set; }


    }
}
